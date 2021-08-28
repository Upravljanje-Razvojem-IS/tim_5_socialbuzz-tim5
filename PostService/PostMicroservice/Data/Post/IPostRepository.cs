using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data.PostRepository
{
    public interface IPostRepository
    {

        List<Post> GetPosts(DateTime? dateOfPublication = null);
        List<Post> GetPostsFromWall(int accountId, DateTime? dateOfPublication = null);
        Post GetPostById(Guid postId);
        List<Post> GetPostByUser(int userID, int accountID);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Guid postId);
        bool SaveChanges();
    }
}
