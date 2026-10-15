using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
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
