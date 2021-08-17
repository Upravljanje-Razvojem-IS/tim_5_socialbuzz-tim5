using ForumService.MockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Repository.UserMock
{
    public interface IUserMockRepository
    {
        List<UserMockDTO> GetAllUsers();

        UserMockDTO GetUserByID(int userID);
    }
}
