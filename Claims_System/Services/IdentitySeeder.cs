using Claims_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Claims_System.Services
{
    public class IdentitySeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Lecturer", "Coordinator", "Manager", "HR" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            // Admin account
            var adminUser = await userManager.FindByEmailAsync("admin@claims.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@claims.com",
                    Email = "admin@claims.com",
                    FullName = "System Admin",
                    EmployeeNumber = 9999
                };

                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Manager"); // Admin = Manager
            }

            // Lecturer user
            var lecturer = await userManager.FindByEmailAsync("lecturer@claims.com");
            if (lecturer == null)
            {
                lecturer = new ApplicationUser
                {
                    UserName = "lecturer@claims.com",
                    Email = "lecturer@claims.com",
                    FullName = "John Lecturer",
                    EmployeeNumber = 1001
                };
                await userManager.CreateAsync(lecturer, "Lecturer@123");
                await userManager.AddToRoleAsync(lecturer, "Lecturer");
            }

            // Coordinator user
            var coordinator = await userManager.FindByEmailAsync("coordinator@claims.com");
            if (coordinator == null)
            {
                coordinator = new ApplicationUser
                {
                    UserName = "coordinator@claims.com",
                    Email = "coordinator@claims.com",
                    FullName = "Jane Coordinator",
                    EmployeeNumber = 2001
                };
                await userManager.CreateAsync(coordinator, "Coordinator@123");
                await userManager.AddToRoleAsync(coordinator, "Coordinator");
            }

            // Manager user (another one for testing)
            var manager = await userManager.FindByEmailAsync("manager@claims.com");
            if (manager == null)
            {
                manager = new ApplicationUser
                {
                    UserName = "manager@claims.com",
                    Email = "manager@claims.com",
                    FullName = "Mark Manager",
                    EmployeeNumber = 3001
                };
                await userManager.CreateAsync(manager, "Manager@123");
                await userManager.AddToRoleAsync(manager, "Manager");
            }
        }
    }
}
