using ShopCET45.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data.Repositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        //get a order 
        Task<IQueryable<Order>> GetOrdersAsync(string username);
    }
}
