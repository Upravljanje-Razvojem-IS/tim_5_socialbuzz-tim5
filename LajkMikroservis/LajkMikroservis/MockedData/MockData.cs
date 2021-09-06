using LajkMikroservis.Entities;
using System.Collections.Generic;

namespace LajkMikroservis.MockedData
{
    public static class MockData
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

        public static readonly List<Post> Posts = new List<Post>
        {
            new Post()
            {
                Id = 1,
                Naziv = "post1"
            },
            new Post()
            {
                Id = 2,
                Naziv = "post2"
            }
        };
    }
}
