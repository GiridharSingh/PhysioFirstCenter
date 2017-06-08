using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.Interface;
using System.Data.Entity;

namespace DAL
{
    public class HistoryDAL : IHistory
    {
        private PhysioDevEntities db = new PhysioDevEntities();

        public int CreateHistory(HistoryModel history)
        {
            //History objHistory = new History();
            history.IsActive = true;
            history.CreatedDate = DateTime.Now.Date;
            history.UpdatedDate = DateTime.Now.Date;

            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    History obj = new History();
                    history.CopyProperties(obj);
                    db.Histories.Add(obj);
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

        public void DeleteHistory(long historyId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var history = db.Histories.Where(m => m.HistoryId == historyId).FirstOrDefault();
                history.IsActive = false;
                history.UpdatedDate = DateTime.Now.Date;
                db.SaveChanges();
            }
        }

        public List<HistoryModel> GetAllHistories()
        {
            List<HistoryModel> lstalllhistory = new List<HistoryModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lsthistories = db.Histories.Where(m => m.IsActive == true).OrderByDescending(m => m.HistoryId).ToList();
                foreach (var history in lsthistories)
                {
                    HistoryModel objhistory = new HistoryModel();
                    history.CopyProperties(objhistory);
                    lstalllhistory.Add(objhistory);
                }
            }
            return lstalllhistory;
        }

        public HistoryModel GetHistory(long? historyId)
        {
            HistoryModel objhistory = new HistoryModel();
            if (historyId != null)
            {
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    var Histor = db.Histories.Where(m => m.HistoryId == historyId).FirstOrDefault();
                    Histor.CopyProperties(objhistory);
                }
            }
            return objhistory;
        }

        public int UpdateHistory(HistoryModel objHistory)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                objHistory.UpdatedDate = DateTime.Now.Date;
                try
                {
                    History history = new History();
                    objHistory.CopyProperties(history);
                    db.Entry(history).State = EntityState.Modified;
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
