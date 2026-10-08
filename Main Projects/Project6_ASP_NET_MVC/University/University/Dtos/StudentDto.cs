using Microsoft.Identity.Client;

namespace University.Dtos
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Standing { get; set; } = string.Empty;
        public double GPA { get; set; }
        public bool EnrollmentStatus { get; set; }

    }
    public class StudentCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int DegreeProgramId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Standing { get; set; } = string.Empty;
        public double GPA { get; set; }
        public bool EnrollmentStatus { get; set; }
    }
    public class StudentUpdateDto : StudentCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
