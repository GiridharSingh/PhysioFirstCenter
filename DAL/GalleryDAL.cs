using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Models;
using System.Web;
using System.IO;
using System.Data.Entity;
using System.Drawing;

namespace DAL
{
    public class GalleryDAL : IGallery
    {

        public int AddPhoto(Photo objPhoto)
        {                      
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    Gallery gallery = new Gallery();
                    objPhoto.CopyProperties(gallery);
                    db.Galleries.Add(gallery);
                    return db.SaveChanges();                    
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;
                }
            }
        }

        public int DeletePhoto(int PhotoId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    var Photo = db.Galleries.Where(m => m.PhotoId == PhotoId).FirstOrDefault();
                    db.Entry(Photo).State = EntityState.Deleted; //Delete forever
                    //galleries.IsActive = false;
                    return db.SaveChanges();                                 
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;
                }
            }
        }

        public List<Photo> GetAllPhoto()
        {
            List<Photo> lstPhotoModel = new List<Photo>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstPhoto = db.Galleries.Where(m => m.IsActive == true).ToList();
                foreach (var photo in lstPhoto)
                {
                    Photo objPhoto = new Photo();
                    photo.CopyProperties(objPhoto);
                    lstPhotoModel.Add(objPhoto);
                }
            }
            return lstPhotoModel;
        }


        public Photo GetPhoto(int? PhotoId)
        {
            Photo objPhotoModel = new Photo();
            if (PhotoId != null)
            {
                using (PhysioDevEntities db = new PhysioDevEntities())
                {
                    var Photo = db.Galleries.Where(m => m.PhotoId == PhotoId).FirstOrDefault();
                    Photo.CopyProperties(objPhotoModel);
                }
            }

            return objPhotoModel;
        }

        public int UpdatePhoto(Photo objPhoto)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                objPhoto.CreatedOn = DateTime.Now.Date;
                try
                {
                    Photo photo = new Photo();
                    objPhoto.CopyProperties(photo);
                    db.Entry(photo).State = EntityState.Modified;                    
                    return db.SaveChanges();
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
