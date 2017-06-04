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
    public class HistoryDAL: IHistory
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        public int CreateHistory(HistoryModel history)
        {
            try
            {
                History objHistory = new History();
                objHistory.IsActive = true;
                objHistory.CreatedDate = DateTime.Now.Date;
                objHistory.UpdatedDate = DateTime.Now.Date;
                MapProperties.CopyProperties(history, objHistory);
                db.Histories.Add(objHistory);
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void DeleteHistory(long historyId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var history = db.Histories.Where(m => m.HistoryId == historyId).FirstOrDefault();
                history.IsActive = false;
                db.SaveChanges();
            }
        }

        public List<HistoryModel> GetAllHistories()
        {
            List<HistoryModel> lstallhistories = new List<HistoryModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lsthistories = db.Histories.Where(m => m.IsActive == true).OrderByDescending(m => m.HistoryId).ToList();
                foreach (var history in lsthistories)
                {
                    HistoryModel objhistory = new HistoryModel();
                    history.CopyProperties(objhistory);
                    lstallhistories.Add(objhistory);
                }
            }
            return lstallhistories;
        }
        
        public HistoryModel GetService(long? historyId)
        {
            HistoryModel objhistory = new HistoryModel();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var service = db.Histories.Where(m => m.HistoryId == historyId).FirstOrDefault();
                service.CopyProperties(objhistory);
            }
            return objhistory;
        }

        public int UpdateService(HistoryModel objHistory)
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
                }
            }
        }
    }
}
