using PostMicroservice.Models;

namespace PostMicroservice.Data
{
    interface IObjectForSaleMockRepository
    {
        ObjectForSaleDTO GetObjectForSaleByID(string objectForSaleID);
        ObjectForSaleDTO GetObjectForSaleByName(string name);
    }
}
