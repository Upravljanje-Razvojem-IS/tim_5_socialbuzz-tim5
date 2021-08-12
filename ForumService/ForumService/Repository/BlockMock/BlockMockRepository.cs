﻿using ForumService.MockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository.BlockMock
{
    public class BlockMockRepository
    {
        public static List<BlockMockDTO> BlockedUsers { get; set; } = new List<BlockMockDTO>();

        public BlockMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            BlockMockDTO block = new BlockMockDTO();//user 1 blokirao 2
            block.BlockingID = Guid.Parse("c48dfbaf-9710-4fa6-8773-4c778ef2d885");
            block.BlockerID = 1;
            block.BlockedID = 2;

            //BlockedUsers.Add(block);
        }

        public List<int> GetBlockedUsers(int userId)
        {
            List<int> blockedUsers = new List<int>();

            var query = from l1 in BlockedUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.BlockedID == userId) //Mene je neko blokirao
                {
                    blockedUsers.Add(v.BlockerID);
                }
                else if (v.BlockerID == userId) //Ja sam nekoga blokirala
                {
                    blockedUsers.Add(v.BlockedID);
                }
            }

            return blockedUsers;
        }

        public bool CheckDidIBlockUser(int userId, int blockedId)
        {
            var query = from l1 in BlockedUsers
                        select l1;

            foreach (var v in query)
            {
                if (v.BlockedID == userId && v.BlockerID == blockedId) //Korisnik je mene blokirao
                {
                    return true;
                }
                else if (v.BlockerID == userId && v.BlockedID == blockedId) //Ja sam blokirala korisnika
                {
                    return true;
                }
            }

            return false;
        }
    }
}