namespace University.Models
{
    public class WaitlistEntry
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int Position { get; set; }
        // change to datetime
        public int OfferedAt { get; set; }
        // change to datetime
        public int ExpiresAt { get; set; }
        public bool Status { get; set; } = false;


    }
}
