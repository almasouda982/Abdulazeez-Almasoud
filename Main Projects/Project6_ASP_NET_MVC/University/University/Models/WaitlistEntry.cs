using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class WaitlistEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Student Id")]
        [Range(1000000000, 9999999999, ErrorMessage = "Student ID must be a 10-digit number.")]
        public long StudentId { get; set; }
        [Required]
        public int CourseSectionId { get; set; }
        [Required]
        [Range(1, 500, ErrorMessage = "Waitlist position must be between 1 and 500.")]
        public int Position { get; set; }
        public DateTime? OfferedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        [Required]
        public bool Status { get; set; } = false;


    }
}
