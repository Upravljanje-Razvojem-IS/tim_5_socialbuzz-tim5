using BlockingService.Entities;
using System.Collections.Generic;

namespace BlockingService.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> Get();
        Location Get(int id);
    }
}
