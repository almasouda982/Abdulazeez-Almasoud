namespace University.Dtos
{
    public class WaitlistEntryDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string? LastName { get; set; } 
        public int? SectionNumber { get; set; }
        public int Position { get; set; }
        public DateTime? OfferedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool Status { get; set; }
    }

    public class WaitlistEntryCreateDto
    {
        public long StudentId { get; set; }
        public int CourseSectionId { get; set; }
        public int Position { get; set; }
        public DateTime? OfferedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool Status { get; set; }
    }

    public class WaitlistEntryUpdateDto : WaitlistEntryCreateDto { 
        public string Uuid { get; set; } = string.Empty;
    }
}
