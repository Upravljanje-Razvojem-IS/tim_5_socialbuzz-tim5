using FriendsService.Entities;
using System.Collections.Generic;

namespace FriendsService.Interfaces
{
    public interface IFriendListRepository
    {
        FriendList Create(FriendList entity);
        IEnumerable<FriendList> Get();
        FriendList Get(int id);
        FriendList Update(FriendList entity);
        FriendList Delete(int id);
    }
}
