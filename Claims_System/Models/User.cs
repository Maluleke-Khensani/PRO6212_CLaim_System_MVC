using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claims_System.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "TEXT")] // SQLite-friendly
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "TEXT")] // SQLite-friendly
        public string PasswordHash { get; set; } // store hashed password

        public int EmployeeNumber { get; set; }
    }
}
