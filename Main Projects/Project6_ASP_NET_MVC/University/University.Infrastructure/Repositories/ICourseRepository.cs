using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course? GetCourseByUuid(string uuid);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(string uuid);
        IEnumerable<Book> GetBooks();
        void AddBookToCourse(int courseId, int bookId);
        void RemoveBookFromCourse(int courseId, int bookId);
    }
}
