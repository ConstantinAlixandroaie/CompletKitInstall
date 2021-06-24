using CompletKitInstall.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CompletKitInstall
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            //The code below is used to seed the database and add the administrator role to one account. 

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //var context = services.GetRequiredService<ApplicationDbContext>();
                //context.Database.Migrate();

                //requires using Microsoft.Extensions.Configuration;
                var config = host.Services.GetRequiredService<IConfiguration>();
                //Set password with the Secret Manager tool.
                //dotnet user-secrets set SeedUserPW<pw>

                //var testUserPw = config["SeedUserPW"];
                try
                {
                    SeedData.Initialize(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.Message, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
