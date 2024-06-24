using CommentService.Models;
using System.Collections.Generic;

namespace CommentService.Data.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> Get();
        Comment Get(int id);
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
