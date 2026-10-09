using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class WaitlistEntryRepository : IWaitlistEntryRepository
    {
        private readonly AppDbContext _db;

        public WaitlistEntryRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<WaitlistEntry> GetAll()
        {
            return _db.WaitlistEntries
                .Include(w => w.Student)
                .Include(w => w.CourseSection)
                .ToList();
        }

        public WaitlistEntry? GetWaitlistEntryByUuid(string uuid)
        {
            return _db.WaitlistEntries
                .Include(w => w.Student)
                .Include(w => w.CourseSection)
                .FirstOrDefault(w => w.Uuid == uuid);
        }

        public void AddWaitlistEntry(WaitlistEntry waitlistEntry)
        {
            _db.WaitlistEntries.Add(waitlistEntry);
            _db.SaveChanges();
        }

        public void UpdateWaitlistEntry(WaitlistEntry waitlistEntry)
        {
            _db.SaveChanges();
        }

        public void DeleteWaitlistEntry(string uuid)
        {
            var waitlistEntry = _db.WaitlistEntries.FirstOrDefault(w => w.Uuid == uuid);
            if (waitlistEntry != null)
            {
                _db.WaitlistEntries.Remove(waitlistEntry);
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
