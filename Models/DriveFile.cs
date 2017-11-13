using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DriveFile
    {
        public String FileId { get; set; }
        public String FileName { get; set; }
        public String Description { get; set; }
        public String MimeType { get; set; }
        public String ThumbnailLink { get; set; }
        public String WebViewLink { get; set; }
        public String WebContentLink { get; set; }        
        public bool? HasThumbnail { get; set; }       
    }
}
