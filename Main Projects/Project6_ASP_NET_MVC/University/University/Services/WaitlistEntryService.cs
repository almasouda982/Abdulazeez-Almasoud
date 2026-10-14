
using University.Dtos;
using University.Models;
using University.Repositories.Base;
using University.Services.Base;

namespace University.Services
{
    public class WaitlistEntryService : IWaitlistEntryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WaitlistEntryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<WaitlistEntryDto> GetAllWaitlistEntries()
        {
            var waitlistEntries = _unitOfWork.WaitlistEntryRepository.GetAll();

            return waitlistEntries.Select(w => new WaitlistEntryDto
            {
                Id = w.Id,
                Uuid = w.Uuid,
                LastName = w.Student != null ? w.Student.LastName : null,
                SectionNumber = w.CourseSection != null ? w.CourseSection.SectionNumber : (int?)null,
                Position = w.Position,
                OfferedAt = w.OfferedAt,
                ExpiresAt = w.ExpiresAt,
                Status = w.Status
            }).ToList();
        }

        public WaitlistEntryUpdateDto? GetWaitlistEntryByUuid(string uuid)
        {
            var wti = _unitOfWork.WaitlistEntryRepository.GetWaitlistEntryByUuid(uuid);
            if (wti == null)
            {
                return null;
            }

            return new WaitlistEntryUpdateDto
            {
                Uuid = wti.Uuid,
                StudentId = wti.StudentId,
                CourseSectionId = wti.CourseSectionId,
                Position = wti.Position,
                OfferedAt = wti.OfferedAt,
                ExpiresAt = wti.ExpiresAt,
                Status = wti.Status
            };
        }

        public void AddWaitlistEntry(WaitlistEntryCreateDto waitlistEntryCreateDto)
        {
            var waitlistEntry = new WaitlistEntry
            {
                StudentId = waitlistEntryCreateDto.StudentId,
                CourseSectionId = waitlistEntryCreateDto.CourseSectionId,
                Position = waitlistEntryCreateDto.Position,
                OfferedAt = waitlistEntryCreateDto.OfferedAt,
                ExpiresAt = waitlistEntryCreateDto.ExpiresAt,
                Status = waitlistEntryCreateDto.Status
            };

            _unitOfWork.WaitlistEntryRepository.AddWaitlistEntry(waitlistEntry);
        }

        public void UpdateWaitlistEntry(WaitlistEntryUpdateDto waitlistEntryUpdateDto)
        {
            var oldwti = _unitOfWork.WaitlistEntryRepository.GetWaitlistEntryByUuid(waitlistEntryUpdateDto.Uuid);
            if (oldwti == null)
            {
                throw new KeyNotFoundException("Waitlist entry not found");
            }

            oldwti.StudentId = waitlistEntryUpdateDto.StudentId;
            oldwti.CourseSectionId = waitlistEntryUpdateDto.CourseSectionId;
            oldwti.Position = waitlistEntryUpdateDto.Position;
            oldwti.OfferedAt = waitlistEntryUpdateDto.OfferedAt;
            oldwti.ExpiresAt = waitlistEntryUpdateDto.ExpiresAt;
            oldwti.Status = waitlistEntryUpdateDto.Status;

            _unitOfWork.WaitlistEntryRepository.UpdateWaitlistEntry(oldwti);
        }

        public void DeleteWaitlistEntry(string uuid)
        {
            var wti = _unitOfWork.WaitlistEntryRepository.GetWaitlistEntryByUuid(uuid);
            if (wti != null)
            {
                _unitOfWork.WaitlistEntryRepository.DeleteWaitlistEntry(uuid);
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _unitOfWork.WaitlistEntryRepository.GetStudents();
        }

        public IEnumerable<CourseSection> GetCourseSections()
        {
            return _unitOfWork.WaitlistEntryRepository.GetCourseSections();
        }
    }
}
