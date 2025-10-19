using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Models
{
    public class ClaimsDbContext : DbContext
    {
        public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options) { }

        public DbSet<LecturerClaim> LecturerClaims { get; set; }
    }
}