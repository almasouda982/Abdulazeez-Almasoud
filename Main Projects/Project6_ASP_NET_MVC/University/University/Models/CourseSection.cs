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
        public int MaxCapacity { get; set; }
        [Required]
        public string DaysOfWeek { get; set; } = "";
        [Required]
        public string TimeSlot { get; set; } = "";
        [Required]
        public  int CourseId { get; set; }

    }
}
