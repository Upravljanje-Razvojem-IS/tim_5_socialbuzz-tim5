﻿using PostMicroservice.Database;
using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext context;

        public PostRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void CreatePost(Post post)
        {
            context.Posts.Add(post);

        }

        public void DeletePost(Guid postId)
        {
            var post = GetPostById(postId);
            context.Remove(post);
        }

        public Post GetPostById(Guid postId)
        {
            return context.Posts.FirstOrDefault(e => e.PostId == postId);

        }

        public List<Post> GetPostByUser(int userID)
        {
            return context.Posts.Where(e => ( e.UserId == userID)).ToList();

        }

        public List<Post> GetPosts(DateTime? dateOfPublication = null )
        {

            return context.Posts.Where(e => (dateOfPublication == null || e.DateOfPublication == dateOfPublication)) .ToList();

        }

      

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;

        }

        public void UpdatePost(Post post)
        {
            // No implementation required because the EF core monitors the entity we extracted from the database
            // and when we change that object and do SaveChanges all changes will be persistent
        }
    }
}
