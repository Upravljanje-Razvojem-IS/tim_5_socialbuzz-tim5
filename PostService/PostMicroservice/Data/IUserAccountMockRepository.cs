using PostMicroservice.Entities;


namespace PostMicroservice.Data
{
    interface IUserAccountMockRepository 
    {
        UserAccountDTO GetAccountByUserAccountID(int userAccountID);
        UserAccountDTO GetAccountByUserName(string userName);
    }

}
