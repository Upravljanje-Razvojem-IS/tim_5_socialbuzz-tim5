using PostMicroservice.Models;

namespace PostMicroservice.Data
{
    public interface IObjectForSaleMockRepository
    {
        ObjectForSaleDto GetObjectForSaleByID(int objectForSaleID);
        ObjectForSaleDto GetObjectForSaleByName(string name);
    }
}
