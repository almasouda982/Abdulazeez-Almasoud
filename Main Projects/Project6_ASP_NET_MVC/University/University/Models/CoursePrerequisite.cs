using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class CoursePrerequisite
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Minimum Grade Required")]
        [RegularExpression(@"^(A[+-]?|B[+-]?|C[+-]?|D[+-]?|F)$", ErrorMessage = "Must be a valid letter grade (e.g., A+, B, C-).")]
        public string? MinGradeRequired { get; set; } = "";
        [Required]
        [DisplayName("Course ID")]
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Prerequisite Course ID")]
        public int PrerequisiteCourseId { get; set; }
    }
}
