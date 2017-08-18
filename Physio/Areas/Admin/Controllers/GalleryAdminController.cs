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
        public ActionResult ShowGallery()
        {
            var photos = _IGallery.GetAllPhoto();
            return View("ShowGallery", photos);
        }
        // GET: Admin/GalleryAdmin
        [HttpGet]
        public ActionResult Create()
        {
            var photo = _IGallery.GetPhoto(null);
            var file = Request.Files;

            return View(photo);
        }
        [HttpPost]
        public ActionResult Create(string Decription )
        {
            bool isSavedSuccessfully = false;
            int fCount = 0;           
            try
            {
                fCount = Request.Files.Count;
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    //fName = file.FileName;
                    var model = new Photo();
                    if (file != null && file.ContentLength > 0)
                    {
                        model.Decription = Decription;//photo.Decription;
                        _IGallery.AddPhoto(model, file);
                        isSavedSuccessfully = true;
                       
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
               // ViewBag.ResponseMsg = "All files saved successful " + fName;
                //return View("Create", photo);
                //return RedirectToAction("ShowGallery");
               return Json(new { Message = fCount+ " files saved successful" });
            }
            else
            {
                //ViewBag.ResponseMsg = "Error in saving file " + fName;
                //return View("Create", photo);
                return Json(new { Message = "Error in saving file" });
                //return RedirectToAction("Create");
            }
        }
        public ActionResult DeletePhoto(int? PhotoId)
        {
            if (PhotoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _IGallery.DeletePhoto(PhotoId.Value);
            return RedirectToAction("ShowGallery");
        }
       
    }
}