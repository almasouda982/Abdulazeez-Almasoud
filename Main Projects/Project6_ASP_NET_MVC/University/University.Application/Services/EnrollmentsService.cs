using University.Application.Dtos;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;
using University.Application.Services.Base;
using Microsoft.AspNetCore.Http;

namespace University.Application.Services
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
                Status = e.Status,
                DocumentUrl = e.DocumentUrl,
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
                Status = enr.Status,
                DocumentUrl = enr.DocumentUrl,
            };
        }

        public void AddEnrollment(EnrollmentCreateDto enrollmentCreateDto)
        {
            string? documentUrl = null;

            if (enrollmentCreateDto.DocumentFile != null)
            {
                var extension = Path.GetExtension(enrollmentCreateDto.DocumentFile.FileName).ToLowerInvariant();
                if (extension != ".pdf")
                {
                    throw new InvalidOperationException("Only PDF files are accepted for transcripts.");
                }
                documentUrl = UploadTranscript(enrollmentCreateDto.DocumentFile);
            }

            var enrollment = new Enrollment
            {
                StudentId = enrollmentCreateDto.StudentId,
                CourseSectionId = enrollmentCreateDto.CourseSectionId,
                FinalGrade = enrollmentCreateDto.FinalGrade,
                LetterGradePoints = enrollmentCreateDto.LetterGradePoints,
                Status = enrollmentCreateDto.Status,
                DocumentUrl = documentUrl
                
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


            if (enrollmentUpdateDto.DocumentFile != null)
            {
                var extension = Path.GetExtension(enrollmentUpdateDto.DocumentFile.FileName).ToLowerInvariant();
                if (extension != ".pdf")
                {
                    throw new InvalidOperationException("Only PDF files only.");
                }
                oldenr.DocumentUrl = UploadTranscript(enrollmentUpdateDto.DocumentFile);
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

        private string UploadTranscript(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "pdfs",
                "documents",
                "transcripts"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/pdfs/documents/transcripts/" + fileName;
        }

    }
}
