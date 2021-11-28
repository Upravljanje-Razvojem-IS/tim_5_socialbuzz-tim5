using ProductsAndServicesMicroservice.DBContexts;
using ProductsAndServicesMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ItemDbContext context;
        private readonly IPastPriceRepository pastPriceRepository;


        public ProductRepository(ItemDbContext context, IPastPriceRepository pastPriceRepository)
        {
            this.context = context;
            this.pastPriceRepository = pastPriceRepository;
        }

        public void CreateProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(Guid id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                context.Remove(product);
            }

        }

        public Product GetProductById(Guid id)
        {
            return context.Products.FirstOrDefault(e => e.ItemId == id);
        }

        public List<Product> GetProducts(string pName = null)
        {
            return context.Products.Where(e => (pName == null || e.Name == pName)).ToList();
        }

        public List<Product> GetProductsByAccountId(Guid id)
        {
            return context.Products.Where(e => (e.AccountId == id)).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        //sacuvam staru cijenu nakon promijene
        public void UpdateProduct(Product oldProduct, Product newProduct)
        {
            oldProduct.Name = newProduct.Name;
            oldProduct.Description = newProduct.Description;
            oldProduct.AccountId = newProduct.AccountId;
            oldProduct.Weight = newProduct.Weight;
            oldProduct.Quantity = newProduct.Quantity;
            oldProduct.ProductionDate = newProduct.ProductionDate;

            if (oldProduct.Price != newProduct.Price)
            {
                PastPrice pastPrice = new PastPrice();
                pastPrice.Price = oldProduct.Price;
                pastPrice.ItemId = oldProduct.ItemId;
                oldProduct.Price = newProduct.Price;
                pastPriceRepository.CreatePastPrice(pastPrice);
            }
        }
    }
}
