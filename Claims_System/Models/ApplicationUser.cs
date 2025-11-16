using Microsoft.AspNetCore.Identity;

namespace Claims_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string Surname { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
