using DAL;
using Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            AboutViewModel about = new AboutViewModel();
            var histories= db.Histories.ToList();
            List<HistoryModel> objallHistory = new List<HistoryModel>();
            List<EmployeeModel> objallemployees = new List<EmployeeModel>();
            foreach (var history in histories )
            {
                HistoryModel objhistory = new HistoryModel();
                history.CopyProperties(objhistory);
                objallHistory.Add(objhistory);
            }
            

            var allemp = db.Employees.ToList();
            foreach(var emp in allemp)
            {
                EmployeeModel employee = new EmployeeModel();
                emp.CopyProperties(employee);
                objallemployees.Add(employee);
            }
            about.lstHistory = objallHistory;
            about.lstEmployees = objallemployees;
            return View(about);
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