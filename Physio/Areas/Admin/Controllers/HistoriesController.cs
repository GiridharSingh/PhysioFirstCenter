using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Models;
using DAL.Interface;

namespace Physio.Areas.Admin.Controllers
{
    public class HistoriesController : Controller
    {        
        private PhysioDevEntities db = new PhysioDevEntities();
        private IHistory _Ihistory;
        public HistoriesController(IHistory objHistory)
        {
            this._Ihistory = objHistory;
        }

        public ActionResult HistoriesIndex()
        {
            var res = _Ihistory.GetAllHistories();
            return View("Index", res);
        }
        
        // GET: Histories/Details/5
        public ActionResult Details(long? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objhistoryModel = _Ihistory.GetHistory(id);
            if (objhistoryModel == null)
            {
                return HttpNotFound();
            }
            return View(objhistoryModel);

        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Histories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryYear,ShortDesc,LongDesc")] HistoryModel history)
        {            
            int retvalue= _Ihistory.CreateHistory(history);
            if (retvalue > 0)
            {
                return RedirectToAction("Index");
            }            
            return View(history);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var historyModel = _Ihistory.GetHistory(id);
            if (historyModel == null)
            {
                return HttpNotFound();
            }
            return View(historyModel);           
        }

        // POST: Histories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,HistoryYear,ShortDesc,LongDesc,IsActive")] HistoryModel objhistory)
        {

            int retvalue = _Ihistory.UpdateHistory(objhistory);
            if (retvalue > 0)
            {
                return RedirectToAction("Index");
            }
            return View(objhistory);
        }

        // GET: Histories/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    History history = _db.Histories.Find(id);
        //    if (history == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(history);

        //}

        // POST: Histories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    History history = db.Histories.Find(id);
        //    db.Histories.Remove(history);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult DeleteHistory(long id)
        {
            _Ihistory.DeleteHistory(id);
            return RedirectToAction("HistoriesIndex");
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
