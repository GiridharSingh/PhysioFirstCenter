using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HistoryModel
    {
        public long HistoryId { get; set; }
        public long HistoryYear { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool IsActive { get; set; }
        public   DateTime?  CreatedDate { get; set; }
        public  long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
