using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace DAL
{
    public class PartialPagesDAL : IPartialPage
    {

        public int AddPartialPage(Models.PartialPageModel obj)
        {            
                obj.CreatedDate = DateTime.Now.Date;
                obj.UpdatedDate = DateTime.Now.Date;
                obj.IsActive = true;
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    try
                    {
                        PartialPage partialpage = new PartialPage();
                        obj.CopyProperties(partialpage);
                        db.PartialPages.Add(partialpage);
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

        public int UpdatePartialPage(Models.PartialPageModel obj)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {

                obj.UpdatedDate = DateTime.Now.Date;

                try
                {
                    PartialPage partialPage = new PartialPage();
                    obj.CopyProperties(partialPage);
                    db.Entry(partialPage).State = EntityState.Modified;
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

        public Models.PartialPageModel GetPartialPage(long? PartialPageId)
        {
            PartialPageModel objPartialPageModel = new PartialPageModel();
            if (PartialPageId != null)
            {
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    var partialPage = db.PartialPages.Where(m => m.PartialPageId == PartialPageId).FirstOrDefault();
                    partialPage.CopyProperties(objPartialPageModel);
                }
            }
            return objPartialPageModel;                  
        }

        public Models.PartialPageModel GetPartialByPageCode(string PageCode)
        {
            PartialPageModel objPartialPageModel = new PartialPageModel();
            if (!string.IsNullOrEmpty(PageCode))
            {
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    var partialPage = db.PartialPages.Where(m => m.PageCode == PageCode).FirstOrDefault();
                    partialPage.CopyProperties(objPartialPageModel);
                }
            }
            return objPartialPageModel;
        }

        public List<Models.PartialPageModel> GetAllPartialPage()
        {
            List<PartialPageModel> lstPartialPageModel = new List<PartialPageModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstPartials = db.PartialPages.Where(m => m.IsActive == true).ToList();
                foreach (var Partial in lstPartials)
                {
                    PartialPageModel obj = new PartialPageModel();
                    Partial.CopyProperties(obj);
                    lstPartialPageModel.Add(obj);
                }
            }
            return lstPartialPageModel;
        }

        public void DeletePartialPage(long PartialPageId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var partialPage = db.PartialPages.Where(m => m.PartialPageId == PartialPageId).FirstOrDefault();
                partialPage.IsActive = false;
                partialPage.UpdatedDate = DateTime.Now.Date;
                db.SaveChanges();
            }
        }
    }
}
