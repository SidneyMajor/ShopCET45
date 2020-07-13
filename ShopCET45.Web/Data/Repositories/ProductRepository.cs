using Microsoft.EntityFrameworkCore;
using ShopCET45.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context): base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User);
        }
    }
}
