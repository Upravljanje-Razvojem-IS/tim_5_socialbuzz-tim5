using CommentService.Models;
using System;

namespace CommentService.Data.Interfaces
{
    public interface IAdRepository
    {
        Ad Get(int adId);
    }
}
