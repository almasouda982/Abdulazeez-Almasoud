namespace University.Application.Dtos
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int? SectionNumber { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
        public decimal LetterGradePoints { get; set; } = 0;
        public bool Status { get; set; }
        public string? DocumentUrl { get; set; } = string.Empty;
    }

    public class EnrollmentCreateDto
    {
        public long StudentId { get; set; }
        public int CourseSectionId { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
        public decimal LetterGradePoints { get; set; } = 0;
        public bool Status { get; set; }
        public string? DocumentFile { get; set; } = string.Empty;
    }

    public class EnrollmentUpdateDto : EnrollmentCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
        public string? DocumentUrl { get; set; }
    }
}
