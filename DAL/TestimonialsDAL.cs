using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;
using DAL.Interface;
using Models;

namespace DAL
{
    public class TestimonialsDAL : ITestimonials
    {
        public int AddTestimonial(TestimonialsModel objTestimonial, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                objTestimonial.PatientImage = target.ToArray();
            }

            objTestimonial.CreatedDate = DateTime.Now.Date;
            objTestimonial.UpdatedDate = DateTime.Now.Date;
            objTestimonial.IsActive = true;
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    Testimonial testimonial = new Testimonial();
                    objTestimonial.CopyProperties(testimonial);
                    db.Testimonials.Add(testimonial);
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;
                }
            }
        }

        public void DeleteTestimonial(int Id)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var testimonial = db.Testimonials.Where(m => m.Id == Id).FirstOrDefault();
                testimonial.IsActive = false;
                testimonial.UpdatedDate = DateTime.Now.Date;
                db.SaveChanges();
            }
        }

        public List<TestimonialsModel> GetAllTestimonial()
        {
            List<TestimonialsModel> lstTestimonialModel = new List<TestimonialsModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstTestimonials = db.Testimonials.Where(m => m.IsActive == true).OrderByDescending(m => m.Id).ToList();
                foreach (var testimonial in lstTestimonials)
                {
                    TestimonialsModel objTestimonial = new TestimonialsModel();
                    testimonial.CopyProperties(objTestimonial);
                    lstTestimonialModel.Add(objTestimonial);
                }
            }
            return lstTestimonialModel;
        }


        public TestimonialsModel GetTestimonial(int? Id)
        {
            TestimonialsModel objTestimonialsModel = new TestimonialsModel();
            if (Id != null)
            {
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    var Testimonial = db.Testimonials.Where(m => m.Id == Id).FirstOrDefault();
                    Testimonial.CopyProperties(objTestimonialsModel);
                }
            }
            
            return objTestimonialsModel;            
        }

        public int UpdateTestimonial(TestimonialsModel objTestimonial)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {

                objTestimonial.UpdatedDate = DateTime.Now.Date;

                try
                {
                    Testimonial testimonial = new Testimonial();
                    objTestimonial.CopyProperties(testimonial);
                    db.Entry(testimonial).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;
                }
            }
        }
    }
}
