using ProductsAndServicesMicroservice.DBContexts;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public class PastPriceRepository : IPastPriceRepository
    {
        private readonly ItemDbContext context;

        public PastPriceRepository(ItemDbContext context)
        {
            this.context = context;
        }

        public void CreatePastPrice(PastPrice pastPrice)
        {
            context.PastPrices.Add(pastPrice);
        }

        public void DeletePastPrice(int id)
        {
            var pastPrice = GetPastPriceById(id);
            if (pastPrice != null)
            {
                context.Remove(pastPrice);
            }

        }

        public PastPrice GetPastPriceById(int id)
        {
            return context.PastPrices.FirstOrDefault(e => e.PastPriceId == id);
        }

        public List<PastPrice> GetPastPriceByItemId(Guid id)
        {
            return context.PastPrices.Where(e => e.ItemId == id).ToList();
        }

        public List<PastPrice> GetPastPrices()
        {
            return context.PastPrices.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePastPrice(PastPrice oldPastPrice, PastPrice newPastPrice)
        {
            oldPastPrice.Price = newPastPrice.Price;
            oldPastPrice.ItemId = newPastPrice.ItemId;
        }
    }
}
