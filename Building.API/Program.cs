using Building.Application.Extensions;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Building.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
               .Build()
               .MigrateDatabase<ApplicationDbContext>((context, services) =>
               {
                  ApplicationContextSeed
                       .SeedAsync(context)
                       .Wait();
               })
              .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
