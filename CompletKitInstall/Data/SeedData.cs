using CompletKitInstall.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Data
{
    public class SeedData
    {
        //it's only used once after that it will be commented and replaced by a 
        //method to promote users to roles by a single administrator account
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CompletKitDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CompletKitDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, "constantin.alixandroaie@gmail.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorsRole);

                // allowed user can create and edit contacts that they create
                //var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@iap.com");
                //await EnsureRole(serviceProvider, managerID, Constants.ArticleManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                 string UserName)
        {
            var testUserPw = "Parola123!";
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(CompletKitDbContext context, string adminID)
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            context.SaveChanges();

        }
    }
}

