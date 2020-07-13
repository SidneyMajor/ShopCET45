using ShopCET45.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data.Repositories
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var product = new List<Product>();

            product.Add(new Product { Id = 1, Name = "Um", Price = 10 });
            product.Add(new Product { Id = 2, Name = "Dois", Price = 10 });
            product.Add(new Product { Id = 3, Name = "Três", Price = 10 });
            product.Add(new Product { Id = 4, Name = "Quarto", Price = 10 });
            product.Add(new Product { Id = 5, Name = "Cinco", Price = 10 });

            return product;
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
