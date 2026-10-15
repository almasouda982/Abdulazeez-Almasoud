using University.Dtos;

namespace University.Services.Base
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses();
        CourseUpdateDto? GetCourseByUuid(string uuid);
        void AddCourse(CourseCreateDto courseCreateDto);
        void UpdateCourse(CourseUpdateDto courseUpdateDto);
        void DeleteCourse(string uuid);
    }
}
