using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data
{
    public class FollowMockRepository : IFollowMockRepository
    {
        public static List<FollowMockDto> FollowedUsers { get; set; } = new List<FollowMockDto>();

        public FollowMockRepository()
        {
            FillData();
        }

        private void FillData()
        {

            FollowMockDto f = new FollowMockDto();
            f.FollowingID = 1;
            f.FollowerID = 7;
            f.FollowedID = 5;

            FollowedUsers.Add(f);
        }

        public bool CheckDoIFollowUser(int followerId, int followedUser)
        {

            var query = from l1 in FollowedUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.FollowerID == followerId && v.FollowedID == followedUser)
                {
                    return true;
                }

            }

            return false;
        }
    }

}
