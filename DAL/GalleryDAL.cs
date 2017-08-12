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

        public int AddPhoto(Photo objPhoto, HttpPostedFileBase file)
        {
            if (file != null)
            {
                using (var img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    //MemoryStream target = new MemoryStream();
                    //file.InputStream.CopyTo(target); ThumbImage
                    //SaveToFolder(img, fileName, extension, new Size(100, 100), model.ThumbPath);
                    objPhoto.ThumbImage = GetResizedImage(img, new Size(100, 100));
                    objPhoto.Image = GetResizedImage(img, new Size(600, 600));
                }
            }

            objPhoto.CreatedOn = DateTime.Now.Date;
            objPhoto.IsActive = true;
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
                    var galleries = db.Galleries.Where(m => m.PhotoId == PhotoId).FirstOrDefault();
                    galleries.IsActive = false;
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
                var lstPhoto = db.Galleries.Where(m => m.IsActive == true).OrderByDescending(m => m.PhotoId).ToList();
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
        private byte[] GetResizedImage(Image img, Size newSize)
        {
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                MemoryStream ms = new MemoryStream();
                newImg.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);//System.Drawing.Imaging.ImageFormat.Gif
                return ms.ToArray();
            }
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }


    }
}
