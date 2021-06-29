using CompletKitInstall.Areas.Identity.Services;
using CompletKitInstall.Authorization;
using CompletKitInstall.Data;
using CompletKitInstall.Data.Acces;
using CompletKitInstall.Data.Acces.CMSRepositories;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CompletKitDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CompletKitDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders(); ;
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                                             .RequireAuthenticatedUser()
                                             .Build();
            }
            );

            //services.AddServerSideBlazor();

            //Added 
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IComplexOperationsHandler, ComplexOperationsHandler>();
            services.AddTransient<ICardContentRepository, CardContentRepository>();
            services.AddTransient<ICarouselContentRepository, CarouselContentRepository>();

            //Authorization
            services.AddScoped<IAuthorizationHandler, ManagerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdministratorsAuthorizationHandler>();

            //EmailSender WithMailkit
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<IMailer, Mailer>();

            //Add MVC to be able to separate front end from backend**no longer needed
            //services.AddMvc();
            //services.AddControllers();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseDefaultFiles();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
