using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ITestimonials
    {
        int AddTestimonial(TestimonialsModel objTestimonial, HttpPostedFileBase file);
        int UpdateTestimonial(TestimonialsModel objTestimonial);
        TestimonialsModel GetTestimonial(int? Id);//GetTestimonial
        List<TestimonialsModel> GetAllTestimonial();
        void DeleteTestimonial(int Id);
    }
}
