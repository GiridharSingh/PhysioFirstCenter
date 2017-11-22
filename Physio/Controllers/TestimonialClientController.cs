using System.Web.Mvc;
using DAL;

namespace Physio.Controllers
{
    public class TestimonialClientController : Controller
    {
        // GET: TestimonialClient
        public ActionResult TestimonialIndex()
        {
            TestimonialsDAL objTestimonialsDAL = new TestimonialsDAL();
            var res = objTestimonialsDAL.GetAllTestimonial();
            return View("Index",res);
        }
    }
}