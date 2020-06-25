using Microsoft.AspNetCore.Identity;

namespace ShopCET45.Web.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }


        public string LastName { get; set; }
    }
}
