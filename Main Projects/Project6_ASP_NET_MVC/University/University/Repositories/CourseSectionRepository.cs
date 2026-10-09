using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class CourseSectionRepository : ICourseSectionRepository
    {
        private readonly AppDbContext _db;

        public CourseSectionRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<CourseSection> GetAll()
        {
            return _db.CourseSections
                .Include(cs => cs.Course)
                .ToList();
        }

        public CourseSection? GetCourseSectionByUuid(string uuid)
        {
            return _db.CourseSections
                .Include(cs => cs.Course)
                .FirstOrDefault(cs => cs.Uuid == uuid);
        }

        public void AddCourseSection(CourseSection courseSection)
        {
            _db.CourseSections.Add(courseSection);
            _db.SaveChanges();
        }

        public void UpdateCourseSection(CourseSection courseSection)
        {
            _db.SaveChanges();
        }

        public void DeleteCourseSection(string uuid)
        {
            var courseSection = _db.CourseSections.FirstOrDefault(cs => cs.Uuid == uuid);
            if (courseSection != null)
            {
                _db.CourseSections.Remove(courseSection);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            return _db.Courses.ToList();
        }
    }
}
