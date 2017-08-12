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
using System.Drawing;
namespace Physio.Areas.Admin.Controllers
{
    [HandleError]
    public class GalleryAdminController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private IGallery _IGallery;
        public GalleryAdminController(IGallery objGallery) 
        {
            this._IGallery = objGallery;
        }
        // GET: Admin/GalleryAdmin
        [HttpGet]
        public ActionResult Create()
        {
            var photo = _IGallery.GetPhoto(null);
            return View(photo);
        }
        [HttpPost]
        public ActionResult Create(Photo photo, IEnumerable<HttpPostedFileBase> files)
        {
            int retvalue = 0;
            if (!ModelState.IsValid)
                return View(photo);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            var model = new Photo();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Decription = photo.Decription;                
                retvalue =retvalue + _IGallery.AddPhoto(model, file);                
                
            }
            return RedirectPermanent("/home");
        }
       
    }
}