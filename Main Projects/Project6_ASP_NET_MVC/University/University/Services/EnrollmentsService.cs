using University.Dtos;
using University.Models;
using University.Repositories.Base;
using University.Services.Base;

namespace University.Services
{
    public class EnrollmentsService : IEnrollmentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EnrollmentDto> GetAllEnrollments()
        {
            var enrollments = _unitOfWork.EnrollmentRepository.GetAll();

            return enrollments.Select(e => new EnrollmentDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                LastName = e.Student != null ? e.Student.LastName : null,
                SectionNumber = e.CourseSection != null ? e.CourseSection.SectionNumber : (int?)null,
                FinalGrade = e.FinalGrade,
                LetterGradePoints = e.LetterGradePoints,
                Status = e.Status
            }).ToList();
        }

        public EnrollmentUpdateDto? GetEnrollmentByUuid(string uuid)
        {
            var enr = _unitOfWork.EnrollmentRepository.GetEnrollmentByUuid(uuid);
            if (enr == null)
            {
                return null;
            }

            return new EnrollmentUpdateDto
            {
                Uuid = enr.Uuid,
                StudentId = enr.StudentId,
                CourseSectionId = enr.CourseSectionId,
                FinalGrade = enr.FinalGrade,
                LetterGradePoints = enr.LetterGradePoints,
                Status = enr.Status
            };
        }

        public void AddEnrollment(EnrollmentCreateDto enrollmentCreateDto)
        {
            var enrollment = new Enrollment
            {
                StudentId = enrollmentCreateDto.StudentId,
                CourseSectionId = enrollmentCreateDto.CourseSectionId,
                FinalGrade = enrollmentCreateDto.FinalGrade,
                LetterGradePoints = enrollmentCreateDto.LetterGradePoints,
                Status = enrollmentCreateDto.Status
            };

            _unitOfWork.EnrollmentRepository.AddEnrollment(enrollment);
        }

        public void UpdateEnrollment(EnrollmentUpdateDto enrollmentUpdateDto)
        {
            var oldenr = _unitOfWork.EnrollmentRepository.GetEnrollmentByUuid(enrollmentUpdateDto.Uuid);
            if (oldenr == null)
            {
                throw new KeyNotFoundException("Enrollment not found");
            }

            oldenr.StudentId = enrollmentUpdateDto.StudentId;
            oldenr.CourseSectionId = enrollmentUpdateDto.CourseSectionId;
            oldenr.FinalGrade = enrollmentUpdateDto.FinalGrade;
            oldenr.LetterGradePoints = enrollmentUpdateDto.LetterGradePoints;
            oldenr.Status = enrollmentUpdateDto.Status;

            _unitOfWork.EnrollmentRepository.UpdateEnrollment(oldenr);
        }

        public void DeleteEnrollment(string uuid)
        {
            var enr = _unitOfWork.EnrollmentRepository.GetEnrollmentByUuid(uuid);
            if (enr != null)
            {
                _unitOfWork.EnrollmentRepository.DeleteEnrollment(uuid);
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _unitOfWork.EnrollmentRepository.GetStudents();
        }

        public IEnumerable<CourseSection> GetCourseSections()
        {
            return _unitOfWork.EnrollmentRepository.GetCourseSections();
        }
    }
}
