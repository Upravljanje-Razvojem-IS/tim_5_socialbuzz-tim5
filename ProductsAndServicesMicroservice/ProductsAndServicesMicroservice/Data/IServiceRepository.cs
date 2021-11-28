using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public interface IServiceRepository
    {
        List<Service> GetServices(string sName = null);
        Service GetServiceById(Guid id);
        void CreateService(Service service);
        void UpdateService(Service oldService, Service newService);
        void DeleteService(Guid id);
        bool SaveChanges();
        List<Service> GetServicesByAccountId(Guid id);
    }
}
