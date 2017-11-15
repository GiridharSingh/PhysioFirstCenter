using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DAL.Interface;
using System.Net;
using Physio.GoogleDrive;

namespace Physio.Controllers
{
    public class GalleryController : Controller
    {
        private IGallery _IGallery;
        public GalleryController(IGallery objGallery)
        {
            this._IGallery = objGallery;
        }
        
        // GET: /Gallery/
        public ActionResult Index()
        {
            
            string secretJsonPath = Path.Combine(Server.MapPath("~/GoogleDrive/client_secret.json"));
            string applicationName ="Drive API .NET Quickstart";  //"PhysioFirstAouth";
            GoogleDriveHelper driveHelper = new GoogleDriveHelper(secretJsonPath, applicationName);
            var listFiles = driveHelper.GetFilesFromGDrive();
            //var model = _IGallery.GetAllPhoto();
            return View(listFiles);
        }
       
	}
}