using University.Data;
using University.Models;

namespace University.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _db;

        public CourseRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Course> GetAll()
        {
            return _db.Courses.ToList();
        }

        public Course? GetCourseByUuid(string uuid)
        {
            return _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
        }

        public void AddCourse(Course course)
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _db.SaveChanges();
        }

        public void DeleteCourse(string uuid)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Uuid == uuid);
            if (course != null)
            {
                _db.Courses.Remove(course);
                _db.SaveChanges();
            }
        }
    }
}
