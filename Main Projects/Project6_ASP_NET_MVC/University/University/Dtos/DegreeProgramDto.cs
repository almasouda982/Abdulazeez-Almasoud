using System.Diagnostics.Contracts;

namespace University.Dtos
{
    public class DegreeProgramDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? CatalogYear { get; set; } = 0;
        public int? TotalCreditsRequired { get; set; } = 0;

    }
    public class DegreeProgramCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int CatalogYear { get; set; } = 0;
        public int TotalCreditsRequired { get; set; } = 0;
    }
    public class DegreeProgramUpdateDto : DegreeProgramCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
