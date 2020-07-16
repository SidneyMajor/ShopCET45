using ShopCET45.Web.Data.Entities;
using ShopCET45.Web.Models;
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


        //get a order temp
        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string username);


        Task AddItemToOrderAsync(AddItemViewModel model, string username);


        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);


        Task DeleteDetailTempAsync(int id);


        Task<bool> ConfirmOrderAsync(string userName);


        Task DelivarOrder(DeliverViewModel model);

        //get a order 
        Task<Order> GetOrdersAsync(int id);
    }
}
