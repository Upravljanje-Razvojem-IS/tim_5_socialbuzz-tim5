using ProductsAndServicesMicroservice.DBContexts;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ItemDbContext context;
        private readonly IPastPriceRepository pastPriceRepository;

        public ServiceRepository(ItemDbContext context, IPastPriceRepository pastPriceRepository)
        {
            this.context = context;
            this.pastPriceRepository = pastPriceRepository;
        }

        public void CreateService(Service service)
        {
            context.Services.Add(service);
        }

        public void DeleteService(Guid id)
        {
            var service = GetServiceById(id);
            if (service != null)
            {
                context.Remove(service);
            }
        }

        public Service GetServiceById(Guid id)
        {
            return context.Services.FirstOrDefault(e => e.ItemId == id);
        }

        public List<Service> GetServices(string sName = null)
        {
            return context.Services.Where(e => (sName == null || e.Name == sName)).ToList();
        }

        public List<Service> GetServicesByAccountId(Guid id)
        {
            return context.Services.Where(e => (e.AccountId == id)).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        //sacuvam staru cijenu nakon promjene 
        public void UpdateService(Service oldService, Service newService)
        {
            oldService.Name = newService.Name;
            oldService.Description = newService.Description;
            oldService.AccountId = newService.AccountId;
            oldService.StartDate = newService.StartDate;
            oldService.EndDate = newService.EndDate;

            if (oldService.Price != newService.Price)
            {
                PastPrice pastPrice = new PastPrice();
                pastPrice.Price = oldService.Price;
                pastPrice.ItemId = oldService.ItemId;
                oldService.Price = newService.Price;
                pastPriceRepository.CreatePastPrice(pastPrice);
            }
        }
    }
}

