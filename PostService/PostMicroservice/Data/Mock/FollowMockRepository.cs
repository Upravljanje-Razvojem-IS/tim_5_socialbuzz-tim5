using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data
{
    public class FollowMockRepository : IFollowMockRepository
    {
        public static List<FollowMockDto> FollowingUsers { get; set; } = new List<FollowMockDto>();

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

            FollowingUsers.Add(f);
        }

        public bool CheckDoIFollowUser(int userId, int followingId)
        {

            var query = from l1 in FollowingUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.FollowerID == userId && v.FollowedID == followingId)
                {
                    return true;
                }

            }

            return false;
        }
    }

}
