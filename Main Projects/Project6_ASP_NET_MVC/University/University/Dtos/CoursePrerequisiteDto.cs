namespace University.Dtos
{
    public class CoursePrerequisiteDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string? MinGradeRequired { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public int PrerequisiteCourseId { get; set; }

    }
    public class CoursePrerequisiteCreateDto
    {
        public string? MinGradeRequired { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int PrerequisiteCourseId { get; set; }
    }
    public class CoursePrerequisiteUpdateDto : CoursePrerequisiteCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
