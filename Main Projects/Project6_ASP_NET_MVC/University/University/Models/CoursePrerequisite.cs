using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class CoursePrerequisite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Minimum Grade Required")]
        public string MinGradeRequired { get; set; } = "";
        public int? CourseId { get; set; }
        public int PrerequisiteCourseId { get; set; }
    }
}
