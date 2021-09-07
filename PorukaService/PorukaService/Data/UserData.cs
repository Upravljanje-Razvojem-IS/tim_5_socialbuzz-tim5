using PorukaService.Entities;
using System.Collections.Generic;

namespace PorukaService.Data
{
    public static class UserData
    {
        public static readonly List<User> Users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "user1",
                Password = "1234"
            },
            new User()
            {
                Id=2,
                Username = "user2",
                Password = "1234"
            }
        };
    }
}
