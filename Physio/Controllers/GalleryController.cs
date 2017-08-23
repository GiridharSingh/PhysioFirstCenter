using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interface;
using System.Net;

namespace Physio.Controllers
{
    public class GalleryController : Controller
    {
        private IGallery _IGallery;
        public GalleryController(IGallery objGallery) 
        {
            this._IGallery = objGallery;
        }
        //
        // GET: /Gallery/
        public ActionResult Index()
        {
            var model = _IGallery.GetAllPhoto();
            return View(model);
        }
       
	}
}