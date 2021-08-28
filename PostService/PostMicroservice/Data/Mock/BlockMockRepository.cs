using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data
{
    public class BlockMockRepository : IBlockMockRepository
    {


        public static List<BlockMockDto> BlockedUsers { get; set; } = new List<BlockMockDto>();

        public BlockMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            BlockMockDto block = new BlockMockDto();
            block.BlockingID = Guid.Parse("c48dfbaf-9710-4fa6-8773-4c778ef2d885");
            block.BlockerID = 5;
            block.BlockedID = 8;

            BlockedUsers.Add(block);
        }

        public bool CheckDidIBlockUser(int userId, int blockedId)
        {
            var query = from user in BlockedUsers
                        select user;

            foreach (var v in query)
            {
                if (v.BlockedID == userId && v.BlockerID == blockedId) 
                {
                    return true;
                }
                else if (v.BlockerID == userId && v.BlockedID == blockedId) 
                {
                    return true;
                }
            }

            return false;
        }

       
    }
}
