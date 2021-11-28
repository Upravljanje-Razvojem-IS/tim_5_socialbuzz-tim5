using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public interface IProductRepository
    {
        List<Product> GetProducts(string pName = null);
        Product GetProductById(Guid id);
        void CreateProduct(Product product);
        void UpdateProduct(Product oldProduct, Product newProduct);
        void DeleteProduct(Guid id);
        bool SaveChanges();
        List<Product> GetProductsByAccountId(Guid id);
    }
}
