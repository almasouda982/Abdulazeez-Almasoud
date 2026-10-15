using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _db;

        public EnrollmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.CourseSection)
                .ToList();
        }

        public Enrollment? GetEnrollmentByUuid(string uuid)
        {
            return _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.CourseSection)
                .FirstOrDefault(e => e.Uuid == uuid);
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            _db.Enrollments.Add(enrollment);
            _db.SaveChanges();
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            _db.SaveChanges();
        }

        public void DeleteEnrollment(string uuid)
        {
            var enrollment = _db.Enrollments.FirstOrDefault(e => e.Uuid == uuid);
            if (enrollment != null)
            {
                _db.Enrollments.Remove(enrollment);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _db.Students.ToList();
        }

        public IEnumerable<CourseSection> GetCourseSections()
        {
            return _db.CourseSections.ToList();
        }
    }
}
