using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceManagerExampleApp
{
    public class SeedDb
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
            await EnsureTestUserAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (!alreadyExists)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
            }

            var userAlreadyExist = await roleManager.RoleExistsAsync(Constants.UserRole);
            if (!userAlreadyExist)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.UserRole));
            }
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            const string email = "conference@manager.local";
            
            var testAdmin = await userManager
                .Users
                .Where(x => x.UserName == email)
                .SingleOrDefaultAsync();
            
            if( testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                Email = email,
                EmailConfirmed = true,
                UserName = email
            };

            await userManager.CreateAsync(testAdmin, "Str0ngP4ssw0rd$!");
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
        
        private static async Task EnsureTestUserAsync(UserManager<IdentityUser> userManager)
        {
            const string email = "conference@user.local";
            
            var testUser = await userManager
                .Users
                .Where(x => x.UserName == email)
                .SingleOrDefaultAsync();
            
            if( testUser != null) return;

            testUser = new IdentityUser
            {
                Email = email,
                EmailConfirmed = true,
                UserName = email
            };

            await userManager.CreateAsync(testUser, "Str0ngP4ssw0rd$!");
            await userManager.AddToRoleAsync(testUser, Constants.UserRole);
        }
    }
}