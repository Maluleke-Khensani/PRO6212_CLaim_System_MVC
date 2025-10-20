using System.ComponentModel.DataAnnotations;

namespace Claims_System.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]  
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } // store hashed password

   
        public int EmployeeNumber { get; set; }
    }
} 