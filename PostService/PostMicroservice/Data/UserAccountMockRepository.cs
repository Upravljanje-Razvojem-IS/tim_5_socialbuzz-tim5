using PostMicroservice.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Data
{
    public class UserAccountMockRepository : IUserAccountMockRepository
    {

        public static List<UserAccountDTO> UserAccounts { get; set; } = new List<UserAccountDTO>();

        public UserAccountMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            UserAccounts.AddRange(new List<UserAccountDTO>
            {
                new UserAccountDTO
                {
                    UserAccountId = 5,
                    UserName = "milica_despotovic"
                },
                 new UserAccountDTO
                {
                    UserAccountId = 7,
                    UserName = "verica_lulic"
                },
                  new UserAccountDTO
                {
                    UserAccountId = 8,
                    UserName = "marija_krivokuca"
                },
   



            });
        }
       

        public UserAccountDTO GetAccountByUserAccountID(int UserAccountID)
        {
            return UserAccounts.FirstOrDefault(e => e.UserAccountId == UserAccountID);

        }

        public UserAccountDTO GetAccountByUserName(string UserName)
        {
            return UserAccounts.FirstOrDefault(e => e.UserName == UserName);

        }
    }
}
