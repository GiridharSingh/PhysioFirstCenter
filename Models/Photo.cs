using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Photo
    {      
        public int PhotoId { get; set; }        
        public String Decription { get; set; }
        public String ImagePath { get; set; }
        //public byte[] Image { get; set; }
        //public byte[] ThumbImage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
