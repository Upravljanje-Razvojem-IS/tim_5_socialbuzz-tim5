using FriendsService.Entities;
using System.Collections.Generic;

namespace FriendsService.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> Get();
        Location Get(int id);
    }
}
