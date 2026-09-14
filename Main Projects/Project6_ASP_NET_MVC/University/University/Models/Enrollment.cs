using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string FinalGrade { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal LetterGradePoints { get; set; } = 0;


    }
}
