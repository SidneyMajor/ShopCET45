using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopCET45.Web.Data.Entities;
using System.Linq;

namespace ShopCET45.Web.Data
{
    public class DataContext : IdentityDbContext<User> //quando usamos o user
    {
        public DbSet<Product> Products { get; set; }


        public DbSet<Country> Countries { get; set; }


        public DbSet<City> Cities { get; set; }


        public DbSet<Order> Orders { get; set; }


        public DbSet<OrderDetail> OrderDetails { get; set; }


        public DbSet<OrderDetailTemp> OrderDetailsTemp { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //Colocar o nome unico para o pais
            modelbuilder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelbuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelbuilder.Entity<OrderDetail>()
               .Property(o => o.Price).HasColumnType("decimal(18,2)");
           

            modelbuilder.Entity<OrderDetailTemp>()
               .Property(o => o.Price).HasColumnType("decimal(18,2)");

            //Cascading Delete Rule
            var cascadeFKs = modelbuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach(var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
