using ForumService.MockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Repository.UserMock
{
    public class UserMockRepository : IUserMockRepository
    {
        public static List<UserMockDTO> Users { get; set; } = new List<UserMockDTO>();

        public UserMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Users.AddRange(new List<UserMockDTO>
            {
                new UserMockDTO//prati ga user 4, on prati 3
                {
                    UserID = 1,
                    Email = "mila20@gmail.com",
                    IsActive = true,
                    Telephone = "0528738628",
                    Username = "mila20milic",
                    Password = "milamila20",
                    DateOfCreation = new DateTime(2021,07,12)
                },
               new UserMockDTO //prati 3
                {
                    UserID = 2,
                    Email = "nemanja@gmail.com",
                    IsActive = true,
                    Telephone = "027/9113228",
                    Username = "Nemanja Maric",
                    Password = "necacar",
                    DateOfCreation = new DateTime(2021,07,20)
                },
                new UserMockDTO //prate ga 1 i 2, on prati 2
                {
                    UserID = 3,
                    Email = "lara.mitrovic.1988@gmail.com",
                    IsActive = true,
                    Telephone = "044/3860668",
                    Username = "Lara Mitrovic",
                    Password = "laralarica",
                    DateOfCreation = new DateTime(2021,07,19)
                },
                 new UserMockDTO //prati 1
                {
                    UserID = 4,
                    Email = "sara@gmail.com",
                    IsActive = true,
                    Telephone = "030/38245691",
                    Username = "Sara Saric",
                    Password = "sarasaric",
                    DateOfCreation = new DateTime(2021,07,25)
                }
            });
        }

        public List<UserMockDTO> GetAllUsers()
        {
            return Users;
        }

        public UserMockDTO GetUserByID(int userID)
        {
            return Users.FirstOrDefault(e => e.UserID == userID);
        }
    }
}
