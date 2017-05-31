using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.IO;
using Models;
using DAL.Interface;
namespace Physio.Areas.Admin.Controllers
{
    [HandleError]
    public class ServicesController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private IService _IService;
        public ServicesController(IService service)
        {
            this._IService = service;
        }
        public ActionResult Index()
        {
            
            var res = _IService.GetAllServices();
            return View("Index",res);
        }

        // GET: Services/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = _IService.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        [ValidateInput(false)] 
        public ActionResult Create([Bind(Include = "ServiceName,ShortDesc,LongDesc,IsActive,Priority")] ServicesModel service, HttpPostedFileBase file)
        {


            int retvalue = _IService.AddService(service, file);
            if (retvalue > 0)
            {
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = _IService.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceName,ShortDesc,LongDesc,IsActive,Priority")] ServicesModel service, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                service.Image = target.ToArray();
            }
            else
            {                
                var oldservice = _IService.GetService(service.ServiceId);
                service.Image = oldservice.Image;
            }

            int retvalue = _IService.UpdateService(service);
            if (retvalue > 0)
            {

                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = _IService.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _IService.DeleteService(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
