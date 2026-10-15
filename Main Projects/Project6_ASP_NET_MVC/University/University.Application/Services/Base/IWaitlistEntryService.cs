using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
{
    public interface IWaitlistEntryService
    {
        IEnumerable<WaitlistEntryDto> GetAllWaitlistEntries();
        WaitlistEntryUpdateDto? GetWaitlistEntryByUuid(string uuid);
        void AddWaitlistEntry(WaitlistEntryCreateDto waitlistEntryCreateDto);
        void UpdateWaitlistEntry(WaitlistEntryUpdateDto waitlistEntryUpdateDto);
        void DeleteWaitlistEntry(string uuid);
        IEnumerable<Student> GetStudents();
        IEnumerable<CourseSection> GetCourseSections();
    }
}
