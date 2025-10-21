using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claims_System.Models
{
    public class LecturerClaim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int EmployeeNumber { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "TEXT")] // SQLite-friendly
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "TEXT")] // SQLite-friendly
        public string ModuleName { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "TEXT")] // SQLite stores DateTime as TEXT
        public DateTime Submitted { get; set; }

        [Required]
        [Range(0, 999)]
        [Column(TypeName = "REAL")] // decimal → REAL
        public decimal HoursWorked { get; set; }

        [Required]
        [Column(TypeName = "REAL")] // decimal → REAL
        public decimal Rate { get; set; } = 500;

        // Computed property, not mapped to DB
        [NotMapped]
        public decimal Total => HoursWorked * Rate;

        [Column(TypeName = "TEXT")]
        public string CoordinatorStatus { get; set; } = "Pending";

        [Column(TypeName = "TEXT")]
        public string ManagerStatus { get; set; } = "Pending";

        [Column(TypeName = "TEXT")]
        public string Notes { get; set; }

        // Document 1
        [Column(TypeName = "TEXT")]
        public string? Document1FileName { get; set; }

        [Column(TypeName = "BLOB")]
        public byte[]? Document1FileData { get; set; }

        // Document 2
        [Column(TypeName = "TEXT")]
        public string? Document2FileName { get; set; }

        [Column(TypeName = "BLOB")]
        public byte[]? Document2FileData { get; set; }
    }
}
