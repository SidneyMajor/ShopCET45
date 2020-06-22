using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();


        Task<T> GetByIdAsync(int id);


        Task CreatAsync(T entity);


        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);


        Task<bool> ExistAsync(int id);

    }
}
