using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Student Id")]
        [Range(1000000000, 9999999999, ErrorMessage = "Student ID must be a 10-digit number.")]
        public long StudentId { get; set; }
        [Required]
        [DisplayName("Course Section Id")]
        public int CourseSectionId { get; set; }
        [Required]
        [DisplayName("Final Grade")]
        [RegularExpression(@"^(A[+-]?|B[+-]?|C[+-]?|D[+-]?|F)$", ErrorMessage = "Must be a valid letter grade (e.g., A+, B, C-).")]
        public string FinalGrade { get; set; } = "";
        [Required]
        public bool Status { get; set; }
        [Required]
        [DisplayName("Letter Grade Points")]
        [Range(0.00, 4.00, ErrorMessage = "Grade points must be between 0.00 and 4.00.")]
        public decimal LetterGradePoints { get; set; } = 0;


    }
}
