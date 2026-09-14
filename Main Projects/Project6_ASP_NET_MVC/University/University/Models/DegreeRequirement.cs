using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class DegreeRequirement
    {
        [Key]
        public int Id { get; set; } = 0;
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; } = "";
        [Required]
        [DisplayName("Required Credits")]
        [Range(40,80, ErrorMessage ="Required credits must be betwen 40 and 80 credits")]
        public int RequiredCredits { get; set; } = 0;
        [Required]
        [DisplayName("Minimum Level")]
        [RegularExpression(@"^(200|300|400)$", ErrorMessage = "Value must be either 200, 300, or 400.")]
        public string MinLevel { get; set; } = "";
    }
}
