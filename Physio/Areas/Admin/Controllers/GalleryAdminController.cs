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
                foreach (string objfile in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[objfile];
                    //Save file content goes here                    
                    var model = new Photo();
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileNameGuid = Guid.NewGuid().ToString();
                        string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        string newFileName = String.Format("{0}{1}", fileNameGuid, extension); 

                        model.Decription = Decription;
                        model.ImagePath = String.Format("/Content/GalleryImages/{0}", newFileName);
                        model.CreatedOn = DateTime.Now;
                        model.IsActive = true;
                        if (SaveToFolder(file, newFileName))
                        {
                            _IGallery.AddPhoto(model);
                            isSavedSuccessfully = true;      
                        }
                                                                           
                    }                    

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {               
               return Json(new { Message = fCount+ " files saved successful" });
            }
            else
            {                
                return Json(new { Message = "Error in saving file" });                
            }
        }
        public ActionResult DeletePhoto(int? PhotoId)
        {
            if (PhotoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Image = _IGallery.GetPhoto(PhotoId);
            if (DeletefileFromFolder(Image.ImagePath))
            {
                _IGallery.DeletePhoto(PhotoId.Value);
            }            
            return RedirectToAction("ShowGallery");
        }
        private bool DeletefileFromFolder( string virtualPath)
        {
            bool isDeleted = false;
            try
            {
                var path = Path.Combine(Server.MapPath(virtualPath));
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    fi.Delete();
                    isDeleted = true;
                }                   
            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
         return isDeleted;
        }
        private bool SaveToFolder(HttpPostedFileBase file, string newFileName)
        {
            bool isSaved = false;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/GalleryImages"));
                    string pathString = System.IO.Path.Combine(path.ToString());                                                       
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                    {
                        System.IO.Directory.CreateDirectory(pathString);
                    }
                    var uploadpath = string.Format("{0}\\{1}", pathString, newFileName);
                    file.SaveAs(uploadpath);
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
            }
            return isSaved;
        }
       
    }
}