using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AppointmentModel
    {
        public long AppointmentId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public  DateTime? AppointmentDate { get; set; }
        public  long? ServiceId { get; set; }
        public  int? TockenNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public long? AssignedTo { get; set; }
        public long? ProceedBy { get; set; }
        public bool? IsProceed { get; set; }
    }
}
