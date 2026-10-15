using University.Application.Dtos;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;
using University.Application.Services.Base;

namespace University.Application.Services
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseSectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CourseSectionDto> GetAllCourseSections()
        {
            var courseSections = _unitOfWork.CourseSectionRepository.GetAll();

            return courseSections.Select(cs => new CourseSectionDto
            {
                Id = cs.Id,
                Uuid = cs.Uuid,
                Code = cs.Course != null ? cs.Course.Code : null,
                Semester = cs.Semester,
                SectionNumber = cs.SectionNumber,
                MaxCapacity = cs.MaxCapacity,
                DaysOfWeek = cs.DaysOfWeek,
                TimeSlot = cs.TimeSlot
            }).ToList();
        }

        public CourseSectionUpdateDto? GetCourseSectionByUuid(string uuid)
        {
            var crs = _unitOfWork.CourseSectionRepository.GetCourseSectionByUuid(uuid);
            if (crs == null)
            {
                return null;
            }

            return new CourseSectionUpdateDto
            {
                Uuid = crs.Uuid,
                CourseId = crs.CourseId,
                Semester = crs.Semester,
                SectionNumber = crs.SectionNumber,
                MaxCapacity = crs.MaxCapacity,
                DaysOfWeek = crs.DaysOfWeek,
                TimeSlot = crs.TimeSlot
            };
        }

        public void AddCourseSection(CourseSectionCreateDto courseSectionCreateDto)
        {
            var courseSection = new CourseSection
            {
                CourseId = courseSectionCreateDto.CourseId,
                Semester = courseSectionCreateDto.Semester,
                SectionNumber = courseSectionCreateDto.SectionNumber,
                MaxCapacity = courseSectionCreateDto.MaxCapacity,
                DaysOfWeek = courseSectionCreateDto.DaysOfWeek,
                TimeSlot = courseSectionCreateDto.TimeSlot
            };

            _unitOfWork.CourseSectionRepository.AddCourseSection(courseSection);
        }

        public void UpdateCourseSection(CourseSectionUpdateDto courseSectionUpdateDto)
        {
            var oldcrs = _unitOfWork.CourseSectionRepository.GetCourseSectionByUuid(courseSectionUpdateDto.Uuid);
            if (oldcrs == null)
            {
                throw new KeyNotFoundException("Course section not found");
            }

            oldcrs.CourseId = courseSectionUpdateDto.CourseId;
            oldcrs.Semester = courseSectionUpdateDto.Semester;
            oldcrs.SectionNumber = courseSectionUpdateDto.SectionNumber;
            oldcrs.MaxCapacity = courseSectionUpdateDto.MaxCapacity;
            oldcrs.DaysOfWeek = courseSectionUpdateDto.DaysOfWeek;
            oldcrs.TimeSlot = courseSectionUpdateDto.TimeSlot;

            _unitOfWork.CourseSectionRepository.UpdateCourseSection(oldcrs);
        }

        public void DeleteCourseSection(string uuid)
        {
            var crs = _unitOfWork.CourseSectionRepository.GetCourseSectionByUuid(uuid);
            if (crs != null)
            {
                _unitOfWork.CourseSectionRepository.DeleteCourseSection(uuid);
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            return _unitOfWork.CourseSectionRepository.GetCourses();
        }
    }
}
