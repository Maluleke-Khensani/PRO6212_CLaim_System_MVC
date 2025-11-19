using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claims_System.Models
{
    public class LecturerProfile
    {
        [Key]
        public string UserId { get; set; }  // matches ApplicationUser.Id


        public string FullName { get; set; }    // copy from ApplicationUser

        [Required]
        public string Email { get; set; }       // copy from ApplicationUser

        [Required]
        public decimal HourlyRate { get; set; } // only lecturers need this

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


    }
}
