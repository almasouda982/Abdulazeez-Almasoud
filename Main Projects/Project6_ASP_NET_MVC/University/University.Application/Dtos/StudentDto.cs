
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Dtos
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        [DisplayName("Full Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Standing { get; set; } = string.Empty;
        public double GPA { get; set; }
        public bool EnrollmentStatus { get; set; }
        public string? ImageUrl { get; set; }

    }
    public class StudentCreateDto
    {
        [DisplayName("Full Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DisplayName("Degree Program Id")]
        public int DegreeProgramId { get; set; }
        [Required]
        [DisplayName("First Name")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "First name contains invalid characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Last name contains invalid characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(Freshman|Sophomore|Junior|Senior|Graduate)$", ErrorMessage = "Standing must be Freshman, Sophomore, Junior, Senior, or Graduate.")]
        public string Standing { get; set; } = string.Empty;
        [Required]
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public double GPA { get; set; }
        [Required]
        public bool EnrollmentStatus { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class StudentUpdateDto : StudentCreateDto
    {
        [Required]
        public string Uuid { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
