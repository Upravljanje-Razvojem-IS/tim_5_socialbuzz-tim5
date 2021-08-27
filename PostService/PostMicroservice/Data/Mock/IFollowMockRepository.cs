using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data
{
    public interface IFollowMockRepository
    {
        public bool CheckDoIFollowUser(int follower, int followedUser);
    }
}
