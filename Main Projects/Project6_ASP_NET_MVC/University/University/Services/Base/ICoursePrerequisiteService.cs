using University.Dtos;
using University.Models;

namespace University.Services.Base
{
    public interface ICoursePrerequisiteService
    {
        IEnumerable<CoursePrerequisiteDto> GetAllCoursePrerequisites();
        CoursePrerequisiteUpdateDto? GetCoursePrerequisiteByUuid(string uuid);
        void AddCoursePrerequisite(CoursePrerequisiteCreateDto coursePrerequisiteCreateDto);
        void UpdateCoursePrerequisite(CoursePrerequisiteUpdateDto coursePrerequisiteUpdateDto);
        void DeleteCoursePrerequisite(string uuid);
        IEnumerable<Course> GetCourses();
    }
}
