using ProductsAndServicesMicroservice.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public class AccountMockRepository : IAccountMockRepository
    {
        public static List<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        public AccountMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Accounts.AddRange(new List<AccountDto>
            {
                new AccountDto
                {
                    AccountId = Guid.Parse("f2d8362a-124f-41a9-a22b-6e35b3a2953c"),
                    FirstName = "Verica",
                    LastName = "Lulic"
                },
                new AccountDto
                {
                    AccountId = Guid.Parse("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                    FirstName = "Milan",
                    LastName = "Lulic"
                },
                new AccountDto
                {
                    AccountId = Guid.Parse("b1d1e043-85c9-4ee1-9eb7-38314c109607"),
                    FirstName = "Anica",
                    LastName = "Lulic"
                },
                new AccountDto
                {
                    AccountId = Guid.Parse("9888cf22-b353-4162-aedc-734ca2dc26a4"),
                    FirstName = "Violeta",
                    LastName = "Lulic"
                }
            });
        }
        public AccountDto GetAccountByFirstName(string firstName)
        {
            return Accounts.FirstOrDefault(e => e.FirstName == firstName);
        }

        public AccountDto GetAccountByLastName(string lastName)
        {
            return Accounts.FirstOrDefault(e => e.LastName == lastName);
        }
    }
}
