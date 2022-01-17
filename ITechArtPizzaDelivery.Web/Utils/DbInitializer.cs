using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ITechArtPizzaDelivery.Web.Utils
{
    public static class DbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                var adminRole = new IdentityRole<int>
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                };
                var result = roleManager.CreateAsync(adminRole).Result;
            }

            if (roleManager.FindByNameAsync("User").Result == null)
            {
                var userRole = new IdentityRole<int>
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                };
                var result = roleManager.CreateAsync(userRole).Result;
            }
        }
        
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var result = userManager.CreateAsync(user, "admin_Pass1").Result;
                if (result.Succeeded)
                {
                    result = userManager.AddToRoleAsync(user, "Admin").Result;
                }
            }
        }
    }
}