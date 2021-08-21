using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data.PostRepository
{
    public interface IPostRepository
    {

        List<Post> GetPosts();
        Post GetPostById(Guid postId);
        void CreatePost(Post post);
        void UpdatePost(Post oldPost, Post newPost);
        void DeletePost(Guid postId);
        bool SaveChanges();
        //List<Picture> GetPicturesByPostId(Guid id);
    }
}
