using University.Models;

namespace University.Repositories
{
    public interface ICoursePrerequisiteRepository
    {
        IEnumerable<CoursePrerequisite> GetAll();
        CoursePrerequisite? GetCoursePrerequisiteByUuid(string uuid);
        void AddCoursePrerequisite(CoursePrerequisite coursePrerequisite);
        void UpdateCoursePrerequisite(CoursePrerequisite coursePrerequisite);
        void DeleteCoursePrerequisite(string uuid);
        IEnumerable<Course> GetCourses();
    }
}
