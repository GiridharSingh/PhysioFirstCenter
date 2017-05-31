using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Models;
using DAL;
using DAL.Interface;

namespace Physio.Areas.Admin.Controllers
{
    [HandleError]
    public class AboutClinicController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private IAboutClinic _IAboutClinic;
        public AboutClinicController(IAboutClinic objAboutClinic)
        {
            this._IAboutClinic = objAboutClinic;
        }
        // GET: Admin/AboutClinic
        public ActionResult Index()
        {
            var res = _IAboutClinic.GetAllAbout();
            return View("Index", res);            
        }
        // GET: Services/Create
        public ActionResult Create()
        {
            var objAboutModel = _IAboutClinic.GetAbout(null);
            return View("AddEditAboutClinic", objAboutModel);
        }
        // GET: Services/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objAboutModel = _IAboutClinic.GetAbout(id);
            if (objAboutModel == null)
            {
                return HttpNotFound();
            }
            return View("AddEditAboutClinic", objAboutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddEdit([Bind(Include = "Id,Name,ShortDesc,LongDesc,IsActive,Priority")] AboutClinicModel objAboutClinic, HttpPostedFileBase file)
        {
            int retvalue = 0;
            if (objAboutClinic.Id == 0 || objAboutClinic.Id == null) // Add About Clinic to system.
            {
                 retvalue = _IAboutClinic.AddAbout(objAboutClinic, file);

            }
            else
            {
                if (file != null)
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    objAboutClinic.Image = target.ToArray();
                }
                else
                {
                    var objOldData = _IAboutClinic.GetAbout(objAboutClinic.Id);
                    //var oldservice = db.Services.Where(m => m.ServiceId == objAboutClinic.Id).FirstOrDefault();
                    objAboutClinic.Image = objOldData.Image;
                }
                retvalue = _IAboutClinic.UpdateAbout(objAboutClinic);            
            }           
            if (retvalue > 0)
            {
                return RedirectToAction("Index");
            }
            return View(objAboutClinic);           
        }
        public ActionResult ClinicDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objAboutModel = _IAboutClinic.GetAbout(id);
            if (objAboutModel == null)
            {
                return HttpNotFound();
            }
            return View(objAboutModel);
        }       
        public ActionResult DeleteAbout(long id)
        {           
            _IAboutClinic.DeleteAbout(id);
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