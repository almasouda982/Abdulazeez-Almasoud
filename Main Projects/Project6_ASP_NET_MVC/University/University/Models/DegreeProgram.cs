using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class DegreeProgram
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Program Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Program name must be between 3 and 100 characters.")]
        public string Name { get; set; } = "";
        [Required]
        [DisplayName("Catalog Year")]
        [Range(2020,2026, ErrorMessage ="Catalog year must be between 2020, and 2026")]
        public int CatalogYear { get; set; } = 0;
        [Required]
        [DisplayName("Total Credits Required")]
        [Range(100, 120, ErrorMessage = "Total credits required must be between 100 and 120")]
        public int TotalCreditsRequired { get; set; } = 0;
    }
}
