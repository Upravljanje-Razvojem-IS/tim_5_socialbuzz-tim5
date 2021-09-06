using IsporukaService.Entities;
using System.Collections.Generic;

namespace IsporukaService.MockedData
{
    public static class UserData
    {
        public static readonly List<User> Users = new List<User>
        {
            new User()
            {
                Id = 1,
                PunoIme = "User user",
                Email = "user1@example.com"
            },
            new User()
            {
                Id = 2,
                PunoIme = "user User",
                Email = "user2@example.com"
            }
        };
    }
}
