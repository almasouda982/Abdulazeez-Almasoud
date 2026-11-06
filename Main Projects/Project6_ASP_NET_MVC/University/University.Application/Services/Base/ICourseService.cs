using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses();
        CourseUpdateDto? GetCourseByUuid(string uuid);
        void AddCourse(CourseCreateDto courseCreateDto);
        void UpdateCourse(CourseUpdateDto courseUpdateDto);
        void DeleteCourse(string uuid);
        IEnumerable<Book> GetBooks();
    }
}
