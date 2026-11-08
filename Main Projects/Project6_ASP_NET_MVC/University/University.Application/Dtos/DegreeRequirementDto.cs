using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Dtos
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
        [Required]
        [DisplayName("Category Name")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [DisplayName("Required Credits")]
        [Range(40, 80, ErrorMessage = "Required credits must be betwen 40 and 80 credits")]
        public int RequiredCredits { get; set; }
        [Required]
        [DisplayName("Minimum Level")]
        [RegularExpression(@"^(200|300|400)$", ErrorMessage = "Value must be either 200, 300, or 400.")]
        public string MinLevel { get; set; } = string.Empty;

    }
    public class DegreeRequirementUpdateDto : DegreeRequirementCreateDto
    {
        public string Uuid { get; set; } = string.Empty;

    }
}
