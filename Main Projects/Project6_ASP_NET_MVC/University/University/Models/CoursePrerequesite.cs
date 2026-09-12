using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class CoursePrerequesite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Minimum Grade Required")]
        public string Min_grade_required { get; set; } = "";
        [Required]
        public int CourseId { get; set; }
    }
}
