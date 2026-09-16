using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class CourseSection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SectionNumber { get; set; }
        [Required]
        [DisplayName("Semester")]
        [RegularExpression(@"^(Fall|Spring|Summer)\s\d{4}$", ErrorMessage = "Semester format must be like 'Fall 2026', 'Spring 2026', or 'Summer 2026'.")]
        public string Semester { get; set; } = "";
        [Required]
        public int MaxCapacity { get; set; }
        [Required]
        public string DaysOfWeek { get; set; } = "";
        [Required]
        public string TimeSlot { get; set; } = "";
        [Required]
        public  int CourseId { get; set; }

    }
}
