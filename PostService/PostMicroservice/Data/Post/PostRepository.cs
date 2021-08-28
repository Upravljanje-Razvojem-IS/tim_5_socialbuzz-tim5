using Microsoft.AspNetCore.Http;
using PostMicroservice.Database;
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
        private readonly IFollowMockRepository followMockRepository;
        private readonly IBlockMockRepository blockMockRepository;

        public PostRepository(AppDbContext context, IFollowMockRepository followMockRepository, IBlockMockRepository blockMockRepository)
        {
            this.context = context;
            this.followMockRepository = followMockRepository;
            this.blockMockRepository = blockMockRepository;
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

        public List<Post> GetPostByUser(int userID, int accountID)
        {
            List<Post> posts = new List<Post>();

            if (blockMockRepository.CheckDidIBlockUser(accountID, userID))
            {

                return null;


            }
            else
            {

                posts = context.Posts.Where(e => (e.UserId == userID)).ToList();

            }

            return posts;
           

        }

        public List<Post> GetPosts(DateTime? dateOfPublication = null )
        {

            return context.Posts.Where(e => (dateOfPublication == null || e.DateOfPublication == dateOfPublication)) .ToList();

        }

        public List<Post> GetPostsFromWall(int accountId,DateTime? dateOfPublication = null)
        {
            List<Post> posts = context.Posts.Where(e => (dateOfPublication == null || e.DateOfPublication == dateOfPublication)).ToList();
            List<Post> postsFromWall = new List<Post>();


            foreach (Post post in posts)
            {
                if (followMockRepository.CheckDoIFollowUser(accountId,post.UserId))
                {
                    postsFromWall.Add(post);
                }
               
            }
            return postsFromWall;
           
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
