using ShopCET45.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context): base(context)
        {

        }
    }
}
