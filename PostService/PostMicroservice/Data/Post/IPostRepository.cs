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
        Post GetPostById(Guid postId);
        List<Post> GetPostByUser(int userID);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Guid postId);
        bool SaveChanges();
    }
}
