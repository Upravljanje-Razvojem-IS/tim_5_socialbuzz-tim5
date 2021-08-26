using PostMicroservice.Database;
using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data.Image
{
    public class PictureRepository : IPictureRepository
    {
        private readonly AppDbContext context;

        public PictureRepository(AppDbContext context)
        {
            this.context = context;
        }


        public void CreatePicture(Entities.Picture picture)
        {
            context.Pictures.Add(picture);
        }

        public void DeletePicture(Guid pictureId)
        {
            var picture = GetPictureById(pictureId);    
            context.Remove(picture);

        }

        public Entities.Picture GetPictureById(Guid pictureId)
        {
            return context.Pictures.FirstOrDefault(e => e.PictureId == pictureId);
        }


        public void UpdatePicture(Entities.Picture picture)
        {
            // No implementation required because the EF core monitors the entity we extracted from the database
            // and when we change that object and do SaveChanges all changes will be persistent

        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Entities.Picture> GetPictures(Guid? postID = null)
        {
            return context.Pictures.Where(e => (postID == null || e.PostID == postID)).ToList();
        }
    }
}
