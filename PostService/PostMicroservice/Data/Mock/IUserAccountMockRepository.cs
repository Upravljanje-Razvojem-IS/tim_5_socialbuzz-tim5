using PostMicroservice.Entities;


namespace PostMicroservice.Data
{
    public interface IUserAccountMockRepository 
    {
        UserAccountDto GetAccountByUserAccountID(int userAccountID);
        UserAccountDto GetAccountByUserName(string userName);
    }

}
