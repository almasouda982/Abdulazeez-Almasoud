namespace University.Dtos
{
    public class DegreeRequirementDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int RequiredCredits { get; set; }
        public string MinLevel { get; set; } = string.Empty;
    }
    public class DegreeRequirementCreateDto
    {
        public int DegreeProgramId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int RequiredCredits { get; set; }
        public string MinLevel { get; set; } = string.Empty;

    }
    public class DegreeRequirementUpdateDto : DegreeRequirementCreateDto
    {
        public string Uuid { get; set; } = string.Empty;

    }
}
