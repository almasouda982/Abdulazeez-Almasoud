using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Course Code")]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Must be 3 letters followed by 3 numbers.")]
        public string Code { get; set; } = "";
        [Required]
        [DisplayName("Course Title")]
        [StringLength(200, MinimumLength =15, ErrorMessage ="Title is must be between 15 - 200 characters ")]
        public string Title { get; set; } = "";
        [Required]
        [DisplayName("Course Credits")]
        [Range(0,6, ErrorMessage ="Credits must be between 0 and 6")]
        public int Credits { get; set; } = 0;
        [DisplayName("Course Description")]
        [StringLength(500, ErrorMessage ="Description must be at most 500 characters long")]
        public string? Description { get; set; } = "";

    }
}
