using ProductsAndServicesMicroservice.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public interface IAccountMockRepository
    {
        AccountDto GetAccountByFirstName(string firstName);
        AccountDto GetAccountByLastName(string lastName);
    }
}
