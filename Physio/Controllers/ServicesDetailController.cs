using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Net;

namespace Physio.Controllers
{
    [HandleError]
    public class ServicesDetailController : Controller
    {
        // GET: ServicesDetail
        private ServicesDAL objServices;
        public ActionResult Index(long? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            objServices = new ServicesDAL();
            var service = objServices.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);            
        }
    }
}