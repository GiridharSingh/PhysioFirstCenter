
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Interface;
using Models;

namespace DAL
{
    public class ServicesDAL : IService
    {
        public int AddService(ServicesModel objSerive, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                objSerive.Image = target.ToArray();
            }
            
            objSerive.CreatedDate = DateTime.Now.Date;
            objSerive.UpdatedDate = DateTime.Now.Date;
            objSerive.IsActive = true;
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    Service service = new Service();
                    objSerive.CopyProperties(service);
                    db.Services.Add(service);
                    db.SaveChanges();
                    return 1;
                }               
                catch (DbEntityValidationException ex)
                {
                    return 0;
                    throw ex;
                    
                }
            }
        }

        public void DeleteService(long ServiceId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var service = db.Services.Where(m => m.ServiceId == ServiceId).FirstOrDefault();
                service.IsActive = false;
                service.UpdatedDate = DateTime.Now.Date;
                db.SaveChanges();
            }
        }

        public List<ServicesModel> GetAllServices()
        {
            List<ServicesModel> lstServicesModel = new List<ServicesModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstServices = db.Services.Where(m => m.IsActive == true).OrderByDescending(m=>m.ServiceId).ToList();
                foreach (var service in lstServices)
                {
                    ServicesModel objservice = new ServicesModel();
                    service.CopyProperties(objservice);
                    lstServicesModel.Add(objservice);
                }
            }
            return lstServicesModel;
        }


        public ServicesModel GetService(long? ServiceId)
        {
            ServicesModel objServicesModel = new ServicesModel();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var service = db.Services.Where(m => m.ServiceId == ServiceId).FirstOrDefault();
                service.CopyProperties(objServicesModel);
            }
            return objServicesModel;
        }

        public int UpdateService(ServicesModel objSerive)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {

                objSerive.UpdatedDate = DateTime.Now.Date;

                try
                {
                    Service service = new Service();
                    objSerive.CopyProperties( service);
                    db.Entry(service).State = EntityState.Modified;
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
