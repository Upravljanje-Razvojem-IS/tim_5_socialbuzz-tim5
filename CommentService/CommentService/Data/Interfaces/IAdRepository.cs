using CommentService.Models;
using System;

namespace CommentService.Data.Interfaces
{
    public interface IAdRepository
    {
        Post Get(int adId);
    }
}
