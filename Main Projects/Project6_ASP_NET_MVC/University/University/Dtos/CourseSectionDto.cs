namespace University.Dtos
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
        public string Semester { get; set; } = string.Empty;
        public int SectionNumber { get; set; }
        public int MaxCapacity { get; set; }
        public string DaysOfWeek { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
    }
    public class CourseSectionUpdateDto : CourseSectionCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
