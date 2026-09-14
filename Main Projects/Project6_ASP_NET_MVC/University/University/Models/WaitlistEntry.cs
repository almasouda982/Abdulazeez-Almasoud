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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The number must be exactly 10 digits.")]
        public string StudentId { get; set; }
        [Required]
        public int Position { get; set; }
        public DateTime? OfferedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        [Required]
        public bool Status { get; set; } = false;


    }
}
