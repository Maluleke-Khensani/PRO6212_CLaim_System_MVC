using Xunit;
using Claims_System.Models;
using Claims_System.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Claims_System_Tests.Models
{
    public class ClaimServiceTest
    {
        // ✅ Use in-memory SQLite for realistic database behavior
        private ClaimsDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseSqlite("Filename=:memory:") // SQLite in-memory DB
                .Options;

            var context = new ClaimsDbContext(options);
            context.Database.OpenConnection();  // important for SQLite memory
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task CreateClaimAsync_Should_Add_Claim()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ClaimService(context);

            var claim = new LecturerClaim
            {
                EmployeeNumber = 1001,
                Username = "lecturer1",
                FullName = "John Doe",
                ModuleName = "Maths 101",
                Month = 1,
                Year = 2025,
                Submitted = DateTime.Now,
                HoursWorked = 10,
                Rate = 500,
                CoordinatorStatus = "Pending",
                ManagerStatus = "Pending",
                Notes = "Test note"
            };

            // Act
            var result = await service.CreateClaimAsync(claim, null, null);

            // Assert
            Assert.True(result);
            Assert.Equal(1, context.LecturerClaims.Count());
        }

        [Fact]
        public async Task DeleteClaimAsync_Should_Remove_Claim()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ClaimService(context);

            var claim = new LecturerClaim
            {
                EmployeeNumber = 2001,
                Username = "lecturer2",
                FullName = "Jane Doe",
                ModuleName = "Physics 201",
                Month = 2,
                Year = 2025,
                Submitted = DateTime.Now,
                HoursWorked = 8,
                Rate = 400,
                CoordinatorStatus = "Pending",
                ManagerStatus = "Pending",
                Notes = "Delete test"
            };

            context.LecturerClaims.Add(claim);
            await context.SaveChangesAsync();

            // Act
            var result = await service.DeleteClaimAsync(claim.ClaimId);

            // Assert
            Assert.True(result);
            Assert.Empty(context.LecturerClaims);
        }

        [Fact]
        public async Task GetAllClaimsAsync_Should_Return_Claims()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ClaimService(context);

            context.LecturerClaims.Add(new LecturerClaim
            {
                EmployeeNumber = 3001,
                Username = "lecturer3",
                FullName = "Mark Smith",
                ModuleName = "Chemistry 301",
                Month = 3,
                Year = 2025,
                Submitted = DateTime.Now,
                HoursWorked = 12,
                Rate = 450,
                CoordinatorStatus = "Pending",
                ManagerStatus = "Pending",
                Notes = "List test"
            });

            await context.SaveChangesAsync();

            // Act
            var claims = await service.GetAllClaimsAsync();

            // Assert
            Assert.NotNull(claims);
            Assert.Single(claims);
        }
    }
}
