using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;

        public StudentRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Student> GetAll()
        {
            return _db.Students
                .Include(s => s.DegreeProgram)
                .ToList();
        }

        public Student? GetStudentByUuid(string uuid)
        {
            return _db.Students
                .Include(s => s.DegreeProgram)
                .FirstOrDefault(s => s.Uuid == uuid);
        }

        public void AddStudent(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            _db.SaveChanges();
        }

        public void DeleteStudent(string uuid)
        {
            var student = _db.Students.FirstOrDefault(s => s.Uuid == uuid);
            if (student != null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
            }
        }

        public IEnumerable<DegreeProgram> GetDegreePrograms()
        {
            return _db.DegreePrograms.ToList();
        }
    }
}
