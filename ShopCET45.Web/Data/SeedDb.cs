using ShopCET45.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private Random _random;
        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if(!_context.Products.Any())
            {
                this.AddProduct("Camisolas SLB");
                this.AddProduct("Calções SLB");
                this.AddProduct("Biquinis SLB");

                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100)
            });
        }
    }
}
