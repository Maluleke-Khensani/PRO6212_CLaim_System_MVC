using System.ComponentModel.DataAnnotations;

namespace Claims_System.Models
{
    public class LoginView
    {
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}