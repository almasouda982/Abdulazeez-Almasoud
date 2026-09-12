namespace University.Models
{
    public class CourseSection
    {
        public int Id { get; set; }
        public int SectionNumber { get; set; }
        public int MaxCapacity { get; set; }
        public string DaysOfWeek { get; set; } = "";
        public string TimeSlot { get; set; } = "";
        public  int CourseId { get; set; }

    }
}
