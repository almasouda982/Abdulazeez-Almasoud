using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
{
    public interface IEnrollmentsService
    {
        IEnumerable<EnrollmentDto> GetAllEnrollments();
        EnrollmentUpdateDto? GetEnrollmentByUuid(string uuid);
        void AddEnrollment(EnrollmentCreateDto enrollmentCreateDto);
        void UpdateEnrollment(EnrollmentUpdateDto enrollmentUpdateDto);
        void DeleteEnrollment(string uuid);
        IEnumerable<Student> GetStudents();
        IEnumerable<CourseSection> GetCourseSections();
    }
}
