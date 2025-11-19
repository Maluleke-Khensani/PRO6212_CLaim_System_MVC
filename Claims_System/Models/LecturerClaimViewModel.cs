using System.ComponentModel.DataAnnotations;

namespace Claims_System.Models
{
    public class LecturerClaimViewModel
    {
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }          // Which month they are claiming

        [Required]
        public int Year { get; set; }           // Which year

        [Required]
        [Range(0, 180)]
        public decimal HoursWorked { get; set; } // Max 180 hours per month

        [Required]
        public decimal Rate { get; set; }


        [Required]
        public string ModuleName { get; set; }   // Module they worked on

        public string Notes { get; set; }        // Optional notes

        public IFormFile? Document1 { get; set; } // Uploaded document 1
        public IFormFile? Document2 { get; set; } // Uploaded document 2
    }

}
