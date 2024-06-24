using CommentService.Data.Interfaces;
using CommentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentService.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> Get()
        {
            return _context.Comments;
        }

        public Comment Get(int id)
        {
            return _context.Comments
                .Where(comment => comment.Id == id)
                .FirstOrDefault();
        }

        public void Create(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
