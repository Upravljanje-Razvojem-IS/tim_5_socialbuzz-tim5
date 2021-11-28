using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public interface IPastPriceRepository
    {
        List<PastPrice> GetPastPrices();

        PastPrice GetPastPriceById(int id);

        void CreatePastPrice(PastPrice pastPrice);

        void UpdatePastPrice(PastPrice oldPastPrice, PastPrice newPastPrice);

        void DeletePastPrice(int id);

        bool SaveChanges();

        List<PastPrice> GetPastPriceByItemId(Guid id);
    }
}
