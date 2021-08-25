using PostMicroservice.Entities;


namespace PostMicroservice.Data
{
    public interface IUserAccountMockRepository 
    {
        UserAccountDTO GetAccountByUserAccountID(int userAccountID);
        UserAccountDTO GetAccountByUserName(string userName);
    }

}
