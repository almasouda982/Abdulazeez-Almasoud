using University.Application.Dtos;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;
using University.Application.Services.Base;

namespace University.Application.Services
{
    public class CoursePrerequisiteService : ICoursePrerequisiteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursePrerequisiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CoursePrerequisiteDto> GetAllCoursePrerequisites()
        {
            var coursePrerequisites = _unitOfWork.CoursePrerequisiteRepository.GetAll();

            return coursePrerequisites.Select(c => new CoursePrerequisiteDto
            {
                Id = c.Id,
                Uuid = c.Uuid,
                MinGradeRequired = c.MinGradeRequired,
                Code = c.Course != null ? c.Course.Code : null,
                PrerequisiteCourseId = c.PrerequisiteCourseId
            }).ToList();
        }

        public CoursePrerequisiteUpdateDto? GetCoursePrerequisiteByUuid(string uuid)
        {
            var crsp = _unitOfWork.CoursePrerequisiteRepository.GetCoursePrerequisiteByUuid(uuid);
            if (crsp == null)
            {
                return null;
            }

            return new CoursePrerequisiteUpdateDto
            {
                Uuid = crsp.Uuid,
                MinGradeRequired = crsp.MinGradeRequired,
                CourseId = crsp.CourseId,
                PrerequisiteCourseId = crsp.PrerequisiteCourseId
            };
        }

        public void AddCoursePrerequisite(CoursePrerequisiteCreateDto coursePrerequisiteCreateDto)
        {
            var coursePrerequisite = new CoursePrerequisite
            {
                MinGradeRequired = coursePrerequisiteCreateDto.MinGradeRequired,
                CourseId = coursePrerequisiteCreateDto.CourseId,
                PrerequisiteCourseId = coursePrerequisiteCreateDto.PrerequisiteCourseId
            };

            _unitOfWork.CoursePrerequisiteRepository.AddCoursePrerequisite(coursePrerequisite);
        }

        public void UpdateCoursePrerequisite(CoursePrerequisiteUpdateDto coursePrerequisiteUpdateDto)
        {
            var oldcrsp = _unitOfWork.CoursePrerequisiteRepository.GetCoursePrerequisiteByUuid(coursePrerequisiteUpdateDto.Uuid);
            if (oldcrsp == null)
            {
                throw new KeyNotFoundException("Course prerequisite not found");
            }

            oldcrsp.MinGradeRequired = coursePrerequisiteUpdateDto.MinGradeRequired;
            oldcrsp.CourseId = coursePrerequisiteUpdateDto.CourseId;
            oldcrsp.PrerequisiteCourseId = coursePrerequisiteUpdateDto.PrerequisiteCourseId;

            _unitOfWork.CoursePrerequisiteRepository.UpdateCoursePrerequisite(oldcrsp);
        }

        public void DeleteCoursePrerequisite(string uuid)
        {
            var crsp = _unitOfWork.CoursePrerequisiteRepository.GetCoursePrerequisiteByUuid(uuid);
            if (crsp != null)
            {
                _unitOfWork.CoursePrerequisiteRepository.DeleteCoursePrerequisite(uuid);
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            return _unitOfWork.CoursePrerequisiteRepository.GetCourses();
        }
    }
}
