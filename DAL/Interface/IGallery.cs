using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Web;

namespace DAL.Interface
{
    public interface IGallery
    {
        int AddPhoto(Photo objPhoto, HttpPostedFileBase file);
        int DeletePhoto(int PhotoId);
        List<Photo> GetAllPhoto();
        Photo GetPhoto(int? PhotoId);
        int UpdatePhoto(Photo objPhoto);
        
    }
}
