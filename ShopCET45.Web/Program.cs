using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopCET45.Web.Data;

namespace ShopCET45.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();

            //CreateWebHostBuilder(args).Build().Run();
        }
        //Uso de Desidn Patter Factory
        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using(var scope= scopeFactory.CreateScope()) // Uso o Using para depois de usar o objeto o eliminar na memória.
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
