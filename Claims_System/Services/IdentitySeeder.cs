using Claims_System.Areas.Identity.Data;
using Claims_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Services
{
    public class IdentitySeeder
    {
        private static ApplicationDbContext _context;

        public static async Task SeedData(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext)
        {
            _context = dbContext;

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        // =====================
        //       ROLES
        // =====================
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Lecturer", "Coordinator", "Manager", "HR" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // =====================
        //       USERS
        // =====================
        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            async Task<ApplicationUser> CreateUserAsync(
                string email,
                string fullName,
                string password,
                string role,
                decimal? hourlyRate = null)
            {
                // Check if exists
                var existingUser = await userManager.FindByEmailAsync(email);
                if (existingUser != null)
                    return existingUser;

                // Create the identity user
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = fullName,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user, password);

                if (!createResult.Succeeded)
                    throw new Exception(
                        "Failed to create user: " +
                        string.Join("; ", createResult.Errors.Select(e => e.Description))
                    );
<<<<<<< Updated upstream

                // Assign role
                await userManager.AddToRoleAsync(user, role);

<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes

                // Assign role
                await userManager.AddToRoleAsync(user, role);

<<<<<<< Updated upstream

            // Admin account
            var adminUser = await userManager.FindByEmailAsync("admin@claims.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
=======
                // Create LecturerProfile ONLY for Lecturers
                if (role == "Lecturer" && hourlyRate.HasValue)
>>>>>>> Stashed changes
=======
                // Create LecturerProfile ONLY for Lecturers
                if (role == "Lecturer" && hourlyRate.HasValue)
>>>>>>> Stashed changes
                {
                    var profile = new LecturerProfile
                    {
                        UserId = user.Id,     // ❤️ NEW: Identity UserId FK
                        FullName = fullName,
                        Email = email,
                        HourlyRate = hourlyRate.Value
                    };

                    _context.LecturerProfiles.Add(profile);
                    await _context.SaveChangesAsync();
                }

                return user;
            }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
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

=======
=======
>>>>>>> Stashed changes
            // SEED USERS HERE
            await CreateUserAsync("hr@claims.com", "Helen Mali", "Hr@1234", "HR");

            // Uncomment when needed:
            // await CreateUserAsync("lecturer@claims.com", "John Lecturer", "Lecturer@123", "Lecturer", 350);
            // await CreateUserAsync("coordinator@claims.com", "Jane Coordinator", "Coordinator@123", "Coordinator");
            // await CreateUserAsync("manager@claims.com", "Mark Manager", "Manager@123", "Manager");
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        }
    }
}
