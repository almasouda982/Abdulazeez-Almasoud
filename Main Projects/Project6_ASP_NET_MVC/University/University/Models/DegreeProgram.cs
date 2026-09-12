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
        public string Name { get; set; } = "";
        [Required]
        [DisplayName("Catalog Year")]
        public int CatalogYear { get; set; } = 0;
        [Required]
        [DisplayName("Total Credits Required")]
        [Range(0, 120, ErrorMessage = "Total credits required must be between 0 and 120")]
        public int TotalCreditsRequired { get; set; } = 0;
    }
}
