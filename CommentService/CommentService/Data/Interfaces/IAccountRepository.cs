using CommentService.Models;
using System;

namespace CommentService.Data.Interfaces
{
    public interface IAccountRepository
    {
        Account Get(Guid accoutnUid);
    }
}
