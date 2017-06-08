using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
   public interface IHistory
    {
        int CreateHistory(HistoryModel history);
        int UpdateHistory(HistoryModel objHistory);
        HistoryModel GetHistory(long? historyId);
        List<HistoryModel> GetAllHistories();
        void DeleteHistory(long historyId);
    }
}
