using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

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
        public string Title { get; set; } = "";
        [Required]
        [DisplayName("Course Credits")]
        [Range(0,4)]
        public int Credits { get; set; } = 0;
        [DisplayName("Course Description")]
        public string? Description { get; set; } = "";

    }
}
