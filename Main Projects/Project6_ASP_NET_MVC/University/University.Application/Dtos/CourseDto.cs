using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; } = 0;
        public string? Description { get; set; } = string.Empty;

        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }

    public class CourseCreateDto
    {

        [Required]
        [DisplayName("Course Code")]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Must be 3 letters followed by 3 numbers.")]
        public string Code { get; set; } = string.Empty;
        [Required]
        [DisplayName("Course Title")]
        [StringLength(200, MinimumLength = 15, ErrorMessage = "Title is must be between 15 - 200 characters ")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [DisplayName("Course Credits")]
        [Range(0, 6, ErrorMessage = "Credits must be between 0 and 6")]
        public int Credits { get; set; } = 0;
        [DisplayName("Course Description")]
        [StringLength(500, ErrorMessage = "Description must be at most 500 characters long")]
        public string? Description { get; set; } = string.Empty;
    }

    public class CourseUpdateDto : CourseCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }

    public class AssignBookToCourseRequest
    {
        public int CourseId { get; set; }
        public int BookId { get; set; }
    }
}
