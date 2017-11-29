using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EmployeeModel
    {
        public long EmpId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DateTime? Joined_date { get; set; }
        public DateTime? CreatedDate { get; set; }    
        public DateTime? UpdatedDate { get; set; }        
        public bool? IsActive { get; set; }
        public string Email { get; set; }       
    }
}
