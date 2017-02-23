using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Interface
{
  public  interface IService
    {
         int AddService(ServicesModel objSerive, HttpPostedFileBase file);
        int UpdateService(ServicesModel objSerive);
        ServicesModel GetService(long? ServiceId);
        List<ServicesModel> GetAllServices();
        void DeleteService(long ServiceId);
    }
}
