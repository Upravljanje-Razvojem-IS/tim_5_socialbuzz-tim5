using FriendsService.Entities;
using FriendsService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FriendsService.Repositories
{
    public class MockLocationRepository : ILocationRepository
    {
        private List<Location> _entities;

        public MockLocationRepository()
        {
            _entities = new List<Location>
            {
                new Location { Id = 1, Country = "Serbia", City = "Novi Sad", Address = "Bulevar Oslobodjenja 5", Zip = "21000" },
                new Location { Id = 2, Country = "Serbia", City = "Belgrade", Address = "Pasterova 4", Zip = "11000" },
                new Location { Id = 3, Country = "Serbia", City = "Nis", Address = "Zelengorska 3", Zip = "18000" },
                new Location { Id = 4, Country = "Serbia", City = "Subotica", Address = "Bajski Put 2", Zip = "24000" },
                new Location { Id = 5, Country = "Serbia", City = "Novi Sad", Address = "Mileve Maric 1", Zip = "21000" },
            };
        }

        public IEnumerable<Location> Get()
        {
            return _entities;
        }

        public Location Get(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }
    }
}
