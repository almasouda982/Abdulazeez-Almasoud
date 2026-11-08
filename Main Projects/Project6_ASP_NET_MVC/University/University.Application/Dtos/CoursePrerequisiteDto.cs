using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Dtos
{
    public class CoursePrerequisiteDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string? MinGradeRequired { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public int PrerequisiteCourseId { get; set; }

    }
    public class CoursePrerequisiteCreateDto
    {
        [DisplayName("Minimum Grade Required")]
        [RegularExpression(@"^(A[+-]?|B[+-]?|C[+-]?|D[+-]?|F)$", ErrorMessage = "Must be a valid letter grade (e.g., A+, B, C-).")]
        public string? MinGradeRequired { get; set; } = string.Empty;
        [Required]
        [DisplayName("Course ID")]
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Prerequisite Course ID")]
        public int PrerequisiteCourseId { get; set; }
    }
    public class CoursePrerequisiteUpdateDto : CoursePrerequisiteCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
