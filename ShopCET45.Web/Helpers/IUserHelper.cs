using Microsoft.AspNetCore.Identity;
using ShopCET45.Web.Data.Entities;
using System.Threading.Tasks;

namespace ShopCET45.Web.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityResult> AddUserAsync(User user, string password);


        Task<User> GetUserByEmailAsync(string email);
    }
}
