﻿using PostMicroservice.Database;
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


        public void CreatePicture(Picture picture)
        {
            context.Images.Add(picture);
        }

        public void DeletePicture(Guid pictureId)
        {
            var picture = GetPictureById(pictureId);    
            context.Remove(picture);

        }

        public Picture GetPictureById(Guid pictureId)
        {
            return context.Images.FirstOrDefault(e => e.ImageId == pictureId);
        }


        public void UpdatePicture(Picture picture)
        {
          
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Picture> GetPictures(Guid? postID = null)
        {
            return context.Images.Where(e => (postID == null || e.PostID == postID)).ToList();
        }
    }
}
