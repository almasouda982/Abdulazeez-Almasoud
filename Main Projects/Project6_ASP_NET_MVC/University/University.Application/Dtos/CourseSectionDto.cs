using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Dtos
{
    public class CourseSectionDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int SectionNumber { get; set; }
        public int MaxCapacity { get; set; }
        public string DaysOfWeek { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
    }
    public class CourseSectionCreateDto
    {
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Semester")]
        [RegularExpression(@"^(Fall|Spring|Summer)\s\d{4}$", ErrorMessage = "Semester format must be like 'Fall 2026', 'Spring 2026', or 'Summer 2026'.")]
        public string Semester { get; set; } = string.Empty;
        [Required]
        [Range(1, 99, ErrorMessage = "Sections must be between 1 and 99.")]
        public int SectionNumber { get; set; }
        [Required]
        [Range(10, 300, ErrorMessage = "Max Capacity must be between 10 and 300")]
        public int MaxCapacity { get; set; }
        public string DaysOfWeek { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
    }
    public class CourseSectionUpdateDto : CourseSectionCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
