using University.Dtos;
using University.Models;

namespace University.Services.Base
{
    public interface ICourseSectionService
    {
        IEnumerable<CourseSectionDto> GetAllCourseSections();
        CourseSectionUpdateDto? GetCourseSectionByUuid(string uuid);
        void AddCourseSection(CourseSectionCreateDto courseSectionCreateDto);
        void UpdateCourseSection(CourseSectionUpdateDto courseSectionUpdateDto);
        void DeleteCourseSection(string uuid);
        IEnumerable<Course> GetCourses();
    }
}
