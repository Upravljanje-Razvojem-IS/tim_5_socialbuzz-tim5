using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostMicroservice.Entities;

namespace PostMicroservice.Data
{
    public interface IPictureRepository
    {
        List<Entities.Picture> GetPictures(Guid? postID = null);
        Entities.Picture GetPictureById(Guid pictureId);
        void CreatePicture(Entities.Picture picture);
        void UpdatePicture(Entities.Picture picture);
        void DeletePicture(Guid pictureId);
        bool SaveChanges();
    }
}
