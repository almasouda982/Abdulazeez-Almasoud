using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FinalGrade { get; set; } = "";
        [Required]
        //change to bool
        public string Status { get; set; } = "";
        [Required]
        public decimal LetterGradePoints { get; set; } = 0;


    }
}
