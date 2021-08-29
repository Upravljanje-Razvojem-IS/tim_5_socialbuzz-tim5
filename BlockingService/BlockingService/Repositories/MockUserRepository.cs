using BlockingService.Entities;
using BlockingService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BlockingService.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _entities;

        public MockUserRepository()
        {
            _entities = new List<User>
            {
                new User { Id = 1, LocationId = 1 },
                new User { Id = 2, LocationId = 2 },
                new User { Id = 3, LocationId = 3 },
                new User { Id = 4, LocationId = 4 },
                new User { Id = 5, LocationId = 5 },
            };
        }

        public IEnumerable<User> Get()
        {
            return _entities;
        }

        public User Get(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }
    }
}
