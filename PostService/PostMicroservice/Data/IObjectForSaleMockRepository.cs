using PostMicroservice.Models;

namespace PostMicroservice.Data
{
    public interface IObjectForSaleMockRepository
    {
        ObjectForSaleDTO GetObjectForSaleByID(int objectForSaleID);
        ObjectForSaleDTO GetObjectForSaleByName(string name);
    }
}
