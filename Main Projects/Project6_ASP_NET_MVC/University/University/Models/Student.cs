using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Degree Program Id")]
        public int DegreeProgramID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = "";
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = "";
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; } = "";
        [Required]
        [Phone]
        public string Phone { get; set; } = "";
        [Required]
        public string Standing { get; set; } = "";
        [Required]
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public double GPA { get; set; } = 0.0;
        [Required]
        public bool EnrollmentStatus { get; set; } = false;
    }
}
