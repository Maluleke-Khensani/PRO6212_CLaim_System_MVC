using Claims_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LecturerClaim> LecturerClaims { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<LecturerClaim>()
        .Property(c => c.HoursWorked)
        .HasPrecision(18, 2); // 18 digits, 2 decimal places

    builder.Entity<LecturerClaim>()
        .Property(c => c.Rate)
        .HasPrecision(18, 2);

    base.OnModelCreating(builder);
    }
}
