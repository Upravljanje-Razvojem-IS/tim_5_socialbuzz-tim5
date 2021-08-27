using BlockingService.Entities;
using System.Collections.Generic;

namespace BlockingService.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int id);
    }
}
