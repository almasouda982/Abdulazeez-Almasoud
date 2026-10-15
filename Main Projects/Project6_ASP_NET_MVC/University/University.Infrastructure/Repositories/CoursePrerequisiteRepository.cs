using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class CoursePrerequisiteRepository : ICoursePrerequisiteRepository
    {
        private readonly AppDbContext _db;

        public CoursePrerequisiteRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<CoursePrerequisite> GetAll()
        {
            return _db.CoursePrerequisites
                .Include(c => c.Course)
                .ToList();
        }

        public CoursePrerequisite? GetCoursePrerequisiteByUuid(string uuid)
        {
            return _db.CoursePrerequisites
                .Include(c => c.Course)
                .FirstOrDefault(c => c.Uuid == uuid);
        }

        public void AddCoursePrerequisite(CoursePrerequisite coursePrerequisite)
        {
            _db.CoursePrerequisites.Add(coursePrerequisite);
            _db.SaveChanges();
        }

        public void UpdateCoursePrerequisite(CoursePrerequisite coursePrerequisite)
        {
            _db.SaveChanges();
        }

        public void DeleteCoursePrerequisite(string uuid)
        {
            var crsp = _db.CoursePrerequisites.FirstOrDefault(c => c.Uuid == uuid);
            if (crsp != null)
            {
                _db.CoursePrerequisites.Remove(crsp);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            return _db.Courses.ToList();
        }
    }
}
