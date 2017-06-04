using DAL;
using System.Web.Mvc;

namespace Physio.Controllers
{
    public class HomeController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        public ActionResult Index()
        {
            Physio.Models.HomeViewModel homeModel = new Physio.Models.HomeViewModel();
            return View(homeModel);
        }

        public ActionResult About()
        {
            Physio.Models.AboutViewModel AboutModel = new Physio.Models.AboutViewModel();                                   
            return View(AboutModel);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Services()
        {
            ServicesDAL service = new ServicesDAL();
            var res = service.GetAllServices();
            return View(res);           
        }
    }
}