using FriendsService.Entities;
using System.Collections.Generic;

namespace FriendsService.Interfaces
{
    public interface IFriendRepository
    {
        Friend Create(Friend entity);
        IEnumerable<Friend> Get();
        Friend Get(int id);
        Friend Update(Friend entity);
        Friend Delete(int id);
    }
}
