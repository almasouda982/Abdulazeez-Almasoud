using University.Models;

namespace University.Repositories
{
    public interface IWaitlistEntryRepository
    {
        IEnumerable<WaitlistEntry> GetAll();
        WaitlistEntry? GetWaitlistEntryByUuid(string uuid);
        void AddWaitlistEntry(WaitlistEntry waitlistEntry);
        void UpdateWaitlistEntry(WaitlistEntry waitlistEntry);
        void DeleteWaitlistEntry(string uuid);
        IEnumerable<Student> GetStudents();
        IEnumerable<CourseSection> GetCourseSections();
    }
}
