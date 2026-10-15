using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface ICourseSectionRepository
    {
        IEnumerable<CourseSection> GetAll();
        CourseSection? GetCourseSectionByUuid(string uuid);
        void AddCourseSection(CourseSection courseSection);
        void UpdateCourseSection(CourseSection courseSection);
        void DeleteCourseSection(string uuid);
        IEnumerable<Course> GetCourses();
    }
}
