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
        public int StudentId { get; set; }
        [Required]
        [DisplayName("Course Section Id")]
        public int CourseSectionId { get; set; }
        [Required]
        [DisplayName("Final Grade")]
        public string FinalGrade { get; set; } = "";
        [Required]
        public bool Status { get; set; }
        [Required]
        [DisplayName("Letter Grade Points")]
        public decimal LetterGradePoints { get; set; } = 0;


    }
}
