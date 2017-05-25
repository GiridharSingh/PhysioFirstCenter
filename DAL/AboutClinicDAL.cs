using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Models;
using System.Web;
using System.IO;

namespace DAL
{
    public class AboutClinicDAL
    {
        public int AddAbout(AboutClinicModel objAbout, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                objAbout.Image = target.ToArray();
            }

            objAbout.CreatedDate = DateTime.Now.Date;
            objAbout.UpdatedDate = DateTime.Now.Date;
            objAbout.IsActive = true;
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    AboutClinic obj = new AboutClinic();
                    objAbout.CopyProperties(obj);
                    db.AboutClinics.Add(obj);
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

        public void DeleteAbout(long AboutId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var objAbout = db.AboutClinics.Where(m => m.Id == AboutId).FirstOrDefault();
                objAbout.IsActive = false;
                db.SaveChanges();
            }
        }

        public List<AboutClinicModel> GetAllAbout()
        {
            List<AboutClinicModel> lstAboutClinicModel = new List<AboutClinicModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstAbouts = db.AboutClinics.Where(m => m.IsActive == true).OrderByDescending(m => m.Id).ToList();
                foreach (var About in lstAbouts)
                {
                    AboutClinicModel objAbout = new AboutClinicModel();
                    About.CopyProperties(objAbout);
                    lstAboutClinicModel.Add(objAbout);
                }
            }
            return lstAboutClinicModel;
        }


        public AboutClinicModel GetAbout(long? AboutId)
        {
            AboutClinicModel objAboutClinicModel = new AboutClinicModel();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var about = db.AboutClinics.Where(m => m.Id == AboutId).FirstOrDefault();
                about.CopyProperties(objAboutClinicModel);
            }
            return objAboutClinicModel;
        }

        public int DeleteAbout(AboutClinicModel objAbout)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {

                objAbout.UpdatedDate = DateTime.Now.Date;

                try
                {
                    AboutClinic objAboutClinic = new AboutClinic();
                    objAbout.CopyProperties(objAboutClinic);
                    db.Entry(objAboutClinic).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
