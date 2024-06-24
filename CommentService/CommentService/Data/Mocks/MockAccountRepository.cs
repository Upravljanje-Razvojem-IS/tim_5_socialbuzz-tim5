using CommentService.Data.Interfaces;
using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentService.Data.Repositories
{
    public class MockAccountRepository : IAccountRepository
    {
        private static List<Account> _accounts = new List<Account>
        {
            new Account
            {
                AccountUid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                FirstName = "Mark",
                LastName = "Junior"
            },

            new Account
            {
                AccountUid = Guid.Parse("b80f0a80-c5f7-4741-b375-e35838a6f713"),
                FirstName = "Jacob",
                LastName = "JZZ"
            },

            new Account
            {
                AccountUid = Guid.Parse("86565dff-d133-478b-93b6-2caab1402e15"),
                FirstName = "Anna",
                LastName = "Nott"
            }
        };

        public Account Get(Guid accoutnUid)
        {
            return _accounts.Where(a => a.AccountUid == accoutnUid).SingleOrDefault();
        }
    }
}
