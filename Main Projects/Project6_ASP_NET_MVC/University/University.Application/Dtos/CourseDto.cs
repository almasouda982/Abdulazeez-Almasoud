using Microsoft.AspNetCore.Http;

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
        public string? ImageUrl {  get; set; } = string.Empty;

    }
    public class CourseCreateDto
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; } = 0;
        public string? Description { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
    public class CourseUpdateDto : CourseCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
