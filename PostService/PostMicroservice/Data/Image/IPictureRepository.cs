using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostMicroservice.Entities;

namespace PostMicroservice.Data
{
    public interface IPictureRepository
    {
        List<Picture> GetPictures(Guid? postID = null);
        Picture GetPictureById(Guid pictureId);
        void CreatePicture(Picture picture);
        void UpdatePicture(Picture picture);
        void DeletePicture(Guid pictureId);
        bool SaveChanges();
    }
}
