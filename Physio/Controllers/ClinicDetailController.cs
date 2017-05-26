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
    public class ClinicDetailController : Controller
    {
        // GET: ClinicDetail
        private AboutClinicDAL objAboutClinic;
        public ActionResult Index(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            objAboutClinic = new AboutClinicDAL();
            var AboutClinic = objAboutClinic.GetAbout(id);
            if (AboutClinic == null)
            {
                return HttpNotFound();
            }
            return View(AboutClinic);
        }
    }
}