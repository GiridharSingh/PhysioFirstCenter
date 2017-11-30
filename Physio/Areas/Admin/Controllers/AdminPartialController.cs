using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL.Interface;
using DAL;

namespace Physio.Areas.Admin.Controllers
{
    [HandleError]
    public class AdminPartialController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private IPartialPage _IPartialPage;
        public AdminPartialController(IPartialPage IPartialPage)
        {
            this._IPartialPage = IPartialPage;
        }
        // GET: Admin/AdminPartial
        public ActionResult PartialPageIndex()
        {
            var res = _IPartialPage.GetAllPartialPage();
            return View("PartialPageIndex", res);            
        }
        public ActionResult AddEditPartial(int? id)
        {
            var objTestimonialsModel = _IPartialPage.GetPartialPage(id);
            return View("AddEditPartial", objTestimonialsModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddEditPartial([Bind(Include = "PartialPageId,PageCode,PageTitle,ShortDesc,LongDesc,IsActive")] PartialPageModel objPageModel)
        {
            int retvalue = 0;
            if (objPageModel.PartialPageId == 0) // Add new page to system.
            {
                retvalue = _IPartialPage.AddPartialPage(objPageModel);
            }
            else
            {
                retvalue = _IPartialPage.UpdatePartialPage(objPageModel);
            }
            if (retvalue > 0)
            {
                return RedirectToAction("PartialPageIndex");
            }
            return View(objPageModel);
        }
        public ActionResult DeletePartial(int PartialPageId)
        {
            _IPartialPage.DeletePartialPage(PartialPageId);
            return RedirectToAction("PartialPageIndex");
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