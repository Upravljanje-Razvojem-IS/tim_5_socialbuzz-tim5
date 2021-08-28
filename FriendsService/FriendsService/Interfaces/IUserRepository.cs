using FriendsService.Entities;
using System.Collections.Generic;

namespace FriendsService.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int id);
    }
}
