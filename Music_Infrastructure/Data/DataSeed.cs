using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Music_Domain.Entities;

namespace Music_Infrastructure.Data
{
    public class DataSeed
    {
        public static async Task DataSeedAdmin (IServiceProvider serviceProvider )
        {
            var userManager =  serviceProvider.GetRequiredService<UserManager<Users>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@gmail.com";
            string adminPassword = "Admin@123";

            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));

            }
         var adminUser = await userManager.FindByEmailAsync(adminEmail);
         if (adminUser == null)
            {
                var users = new Users
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };
                var result = await userManager.CreateAsync(users, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users, "admin");
                }

            }

        }
    }
}