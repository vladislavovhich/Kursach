using Lab_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab_5.Data
{
    public static class RolesInitializer
    {
        public static async Task<int> Initialize(HttpContext context)
        {

            UserManager<User> userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "Admin@gmail.com";
            string password = "Zxcvbnm_123";

            if (await roleManager.FindByNameAsync("MainAdmin") == null)
            {

                await roleManager.CreateAsync(new IdentityRole("MainAdmin"));

                User admin = new User { Email = adminEmail, UserName = adminEmail };

                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "MainAdmin");
                }

            }
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            return 0;            
        }
    }
}