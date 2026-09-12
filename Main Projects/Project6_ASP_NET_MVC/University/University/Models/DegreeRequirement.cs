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
        public int RequiredCredits { get; set; } = 0;
        [Required]
        [DisplayName("Minimum Level")]
        public string MinLevel { get; set; } = "";
    }
}
