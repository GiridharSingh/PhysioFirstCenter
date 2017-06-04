using System.Collections.Generic;
using Models;
using DAL;
namespace Physio.Models
{
    public class HomeViewModel
    {
        private readonly List<ServicesModel> _Services = new List<ServicesModel>();
        private readonly List<AboutClinicModel> _Abouts = new List<AboutClinicModel>();
        public HomeViewModel()
        { 
            ServicesDAL objServicesDAL = new ServicesDAL();
            AboutClinicDAL objAboutClinicDAL = new AboutClinicDAL();
            _Services = objServicesDAL.GetAllServices();
            _Abouts = objAboutClinicDAL.GetAllAbout();                
        }
        public  List<ServicesModel> lstServices 
        {
            get { return _Services; }
            
        }

        public List<AboutClinicModel> lstAboutClinic 
        {
            get { return _Abouts; }
        }
    }
}
