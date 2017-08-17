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
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                string fName = file.FileName;
            }
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

        [HttpPost]
        public ActionResult SaveUploadedFile(Photo photo)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    var model = new Photo();
                    if (file != null && file.ContentLength > 0)
                    {
                        model.Decription = photo.Decription;
                        _IGallery.AddPhoto(model, file);
                        //var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        //var fileName1 = Path.GetFileName(file.FileName);

                        //bool isExists = System.IO.Directory.Exists(pathString);

                        //if (!isExists)
                        //    System.IO.Directory.CreateDirectory(pathString);

                        //var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        //file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {                
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
       
    }
}