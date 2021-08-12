using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository.BlockMock
{
    public interface IBlockMockRepository
    {
        List<int> GetBlockedUsers(int userId);

        /// <summary>
        /// Provera da li je ulogovani korisnik blokirao drugog korisnika
        /// </summary>
        /// <param name="userId">Id ulogovanog</param>
        /// <param name="blockedId">Id korisnika za kog se proverava da li je blokiran</param>
        /// <returns></returns>
        public bool CheckDidIBlockUser(int userId, int blockedId);
    }
}
