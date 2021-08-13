using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository.FollowingMock
{
    public interface IFollowingMockRepository
    {
        List<int> GetFollowedUsers(int userId);

        /// <summary>
        /// Provera koga prati ulogovani korisnik
        /// </summary>
        /// <param name="userId">Id ulogovanog korisnika</param>
        /// <param name="followingId">Id korisnika za kog se proverava da li ga user prati</param>
        /// <returns></returns>
        public bool CheckDoIFollowUser(int userId, int followingId);
    }
}
