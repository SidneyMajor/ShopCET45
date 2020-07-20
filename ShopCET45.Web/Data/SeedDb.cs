using Microsoft.AspNetCore.Identity;
using ShopCET45.Web.Data.Entities;
using ShopCET45.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private readonly Random _random;

        public readonly  IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            if (!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Faro" });
                cities.Add(new City { Name = "Coimbra" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });

                await _context.SaveChangesAsync();

            }

            var user = await _userHelper.GetUserByEmailAsync("sidney-major@portugal.pt");

            if(user == null)
            {
                user = new User
                {
                    FirstName = "sidney",
                    LastName = "major",
                    Email = "sidney-major@portugal.pt",
                    UserName = "sidney-major@portugal.pt",
                    PhoneNumber = "211234567",
                    Address="Rua Maravilha 31 2º Dº",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");//Criar o user com aquela password
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

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
