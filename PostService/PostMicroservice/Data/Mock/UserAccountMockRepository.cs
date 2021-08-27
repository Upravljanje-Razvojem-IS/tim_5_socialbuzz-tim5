using PostMicroservice.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Data
{
    public class UserAccountMockRepository : IUserAccountMockRepository
    {

        public static List<UserAccountDto> UserAccounts { get; set; } = new List<UserAccountDto>();

        public UserAccountMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            UserAccounts.AddRange(new List<UserAccountDto>
            {
                new UserAccountDto
                {
                    UserAccountId = 5,
                    UserName = "milica_despotovic"
                },
                 new UserAccountDto
                {
                    UserAccountId = 7,
                    UserName = "verica_lulic"
                },
                  new UserAccountDto
                {
                    UserAccountId = 8,
                    UserName = "marija_krivokuca"
                },
   



            });
        }
       

        public UserAccountDto GetAccountByUserAccountID(int userAccountID)
        {
            return UserAccounts.FirstOrDefault(e => e.UserAccountId == userAccountID);

        }

        public UserAccountDto GetAccountByUserName(string userName)
        {
            return UserAccounts.FirstOrDefault(e => e.UserName == userName);

        }
    }
}
