using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Web;

namespace DAL.Interface
{
    interface IAboutClinic
    {
        int AddAbout(AboutClinicModel objAbout, HttpPostedFileBase file);
        int UpdateAbout(AboutClinicModel objAbout);
        AboutClinicModel GetAbout(long? AboutId);
        List<AboutClinicModel> GetAllAbout();
        void DeleteAbout(long AboutId);
    }
}
