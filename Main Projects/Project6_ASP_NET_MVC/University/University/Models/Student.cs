using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Student
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Degree Program Id")]
        public int DegreeProgramId { get; set; }
        [Required]
        [DisplayName("First Name")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "First name contains invalid characters.")]
        public string FirstName { get; set; } = "";
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Last name contains invalid characters.")]
        public string LastName { get; set; } = "";
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; } = "";
        [Required]
        [Phone]
        public string Phone { get; set; } = "";
        [Required]
        [RegularExpression(@"^(Freshman|Sophomore|Junior|Senior|Graduate)$", ErrorMessage = "Standing must be Freshman, Sophomore, Junior, Senior, or Graduate.")]
        public string Standing { get; set; } = "";
        [Required]
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public double GPA { get; set; } = 0.0;
        [Required]
        public bool EnrollmentStatus { get; set; } = false;
    }
}
