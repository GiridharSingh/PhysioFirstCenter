using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TestimonialsModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientDesignation { get; set; }
        public string PatientExperience { get; set; }
        public byte[] PatientImage { get; set; }
        //public byte[] BannerImage { get; set; }        
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
