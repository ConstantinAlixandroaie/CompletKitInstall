using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.Repositories;
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
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CompletKitDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddServerSideBlazor();
            
            //Added 
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
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
                app.UseDatabaseErrorPage();
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
                // Normally, you would have .MapBlazorHub() and /MapFallbackToPage("/_Host") here.
                //
                // The change below puts your Blazor content under /Blazor (YourSite.com/Blazor/SomePage). This serves a few purposes:
                //  - Eliminates the possibility of conflicts with existing MVC routes.
                //  - Ordinarily, Blazor takes over the default route (YourSite.com with no path) which can be problematic.
                //    Our goal is to avoid interfering with existing MVC behavior.
                // 
                // Some day, if the entire MVC app is ever completely re-worked in Blazor, you could change this
                // back to the typical settings, tweak a few other minor changes in _Host that support this, and perhaps have a party.
                endpoints.MapBlazorHub("/Blazor/_blazor");
                endpoints.MapFallbackToPage("~/Blazor/{*clientroutes:nonfile}", "/Blazor/_Host");
            });
        }
    }
}
