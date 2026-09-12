using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace University.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Course Code")]
        public string Code { get; set; } = "";
        [Required]
        [DisplayName("Course Title")]
        public string Title { get; set; } = "";
        [Required]
        [DisplayName("Course Credits")]
        public int Credits { get; set; } = 0;
        [DisplayName("Course Description")]
        public string Description { get; set; } = "";

    }
}
