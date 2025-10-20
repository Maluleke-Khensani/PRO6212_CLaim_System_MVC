using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Models
{
    public class ClaimsDbContext : DbContext
    {
        public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options) { }

        public DbSet<LecturerClaim> LecturerClaims { get; set; }


        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData
            (
                new User { UserId = 1, Username = "lecturer1", PasswordHash = "pass123", EmployeeNumber = 1001 },
                new User { UserId = 2, Username = "coordinator1", PasswordHash = "admin123", EmployeeNumber = 2001 },
                new User { UserId = 3, Username = "lecturer2", PasswordHash = "pass456", EmployeeNumber = 1002 },
                new User { UserId = 4, Username = "manager1", PasswordHash = "manager123", EmployeeNumber = 3001 }
            );


        }
    }
}