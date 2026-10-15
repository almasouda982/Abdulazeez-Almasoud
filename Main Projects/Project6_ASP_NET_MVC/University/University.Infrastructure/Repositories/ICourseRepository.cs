using University.Models;

namespace University.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course? GetCourseByUuid(string uuid);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(string uuid);
    }
}
