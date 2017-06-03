using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Models;
using DAL;
using DAL.Interface;

namespace Physio.Areas.Admin.Controllers
{
    [HandleError]
    public class TestimonialController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private ITestimonials _ITestimonials;
        public TestimonialController(ITestimonials objTestimonials) 
        {
            this._ITestimonials = objTestimonials;
        }
        // GET: Admin/Testimonial
        public ActionResult ShowTestimonials()
        {
            var res = _ITestimonials.GetAllTestimonial();
            return View("ShowTestimonials", res);            
        }
        // GET: Testimonial/Create
        public ActionResult Create()
        {
            var objTestimonialsModel = _ITestimonials.GetTestimonial(null);
            return View("AddEditTestimonials", objTestimonialsModel);
        }
        // GET: Testimonial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objTestimonialsModel = _ITestimonials.GetTestimonial(id);
            if (objTestimonialsModel == null)
            {
                return HttpNotFound();
            }
            return View("AddEditTestimonials", objTestimonialsModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddEdit([Bind(Include = "Id,PatientName,PatientDesignation,PatientExperience,IsActive")] TestimonialsModel objTestimonialsModel, HttpPostedFileBase file)
        {
            int retvalue = 0;
            if (objTestimonialsModel.Id == 0 || objTestimonialsModel.Id == null) // Add About Clinic to system.
            {
                retvalue = _ITestimonials.AddTestimonial(objTestimonialsModel, file);

            }
            else
            {
                if (file != null)
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    objTestimonialsModel.PatientImage = target.ToArray();
                }
                else
                {
                    var objOldData = _ITestimonials.GetTestimonial(objTestimonialsModel.Id);
                    //var oldservice = db.Services.Where(m => m.ServiceId == objAboutClinic.Id).FirstOrDefault();
                    objTestimonialsModel.PatientImage = objOldData.PatientImage;
                }
                retvalue = _ITestimonials.UpdateTestimonial(objTestimonialsModel);
            }
            if (retvalue > 0)
            {
                return RedirectToAction("ShowTestimonials");
            }
            return View(objTestimonialsModel);
        }
        //Need To modify according to Testimonial
        //public ActionResult ClinicDetails(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var objAboutModel = _IAboutClinic.GetAbout(id);
        //    if (objAboutModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(objAboutModel);
        //}
        //public ActionResult DeleteAbout(long id)
        //{
        //    _IAboutClinic.DeleteAbout(id);
        //    return RedirectToAction("Index");
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        }


       
    }
}