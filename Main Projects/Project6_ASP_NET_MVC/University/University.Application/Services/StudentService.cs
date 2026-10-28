using University.Application.Dtos;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;
using University.Application.Services.Base;
using Microsoft.AspNetCore.Http;

namespace University.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            var students = _unitOfWork.StudentRepository.GetAll();

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Uuid = s.Uuid,
                Name = s.DegreeProgram != null ? s.DegreeProgram.Name : "",
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Standing = s.Standing,
                GPA = s.GPA,
                EnrollmentStatus = s.EnrollmentStatus,
                ImageUrl = s.ImageUrl,
            }).ToList();
        }

        public StudentUpdateDto? GetStudentByUuid(string uuid)
        {
            var std = _unitOfWork.StudentRepository.GetStudentByUuid(uuid);
            if (std == null)
            {
                return null;
            }

            return new StudentUpdateDto
            {
                Uuid = std.Uuid,
                DegreeProgramId = std.DegreeProgramId,
                FirstName = std.FirstName,
                LastName = std.LastName,
                Email = std.Email,
                Phone = std.Phone,
                Standing = std.Standing,
                GPA = std.GPA,
                EnrollmentStatus = std.EnrollmentStatus,
                ImageUrl = std.ImageUrl,
            };
        }

        public void AddStudent(StudentCreateDto studentCreateDto)
        {
            string? imageUrl = null;

            //file upload logic (img)
            if (studentCreateDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(studentCreateDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only Image files are accepted");
                }
                imageUrl = UploadImage(studentCreateDto.ImageFile);
            }

            var newStudent = new Student
            {
                Uuid = Guid.NewGuid().ToString(),
                DegreeProgramId = studentCreateDto.DegreeProgramId,
                FirstName = studentCreateDto.FirstName,
                LastName = studentCreateDto.LastName,
                Email = studentCreateDto.Email,
                Phone = studentCreateDto.Phone,
                Standing = studentCreateDto.Standing,
                GPA = studentCreateDto.GPA,
                EnrollmentStatus = studentCreateDto.EnrollmentStatus,
                ImageUrl = imageUrl,
            };

            _unitOfWork.StudentRepository.AddStudent(newStudent);
        }

        public void UpdateStudent(StudentUpdateDto studentUpdateDto)
        {
            var oldstd = _unitOfWork.StudentRepository.GetStudentByUuid(studentUpdateDto.Uuid);
            if (oldstd == null)
            {
                throw new KeyNotFoundException("Student not found");
            }


            //file upload logic (img)
            if (studentUpdateDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(studentUpdateDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only Image files are accepted");
                }
                oldstd.ImageUrl = UploadImage(studentUpdateDto.ImageFile);
            }


            oldstd.DegreeProgramId = studentUpdateDto.DegreeProgramId;
            oldstd.FirstName = studentUpdateDto.FirstName;
            oldstd.LastName = studentUpdateDto.LastName;
            oldstd.Email = studentUpdateDto.Email;
            oldstd.Phone = studentUpdateDto.Phone;
            oldstd.Standing = studentUpdateDto.Standing;
            oldstd.GPA = studentUpdateDto.GPA;
            oldstd.EnrollmentStatus = studentUpdateDto.EnrollmentStatus;


            _unitOfWork.StudentRepository.UpdateStudent(oldstd);
        }

        public void DeleteStudent(string uuid)
        {
            var std = _unitOfWork.StudentRepository.GetStudentByUuid(uuid);
            if (std != null)
            {
                _unitOfWork.StudentRepository.DeleteStudent(uuid);
            }
        }

        public IEnumerable<DegreeProgram> GetDegreePrograms()
        {
            return _unitOfWork.StudentRepository.GetDegreePrograms();
        }


        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "students"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/students/" + fileName;
        }
    }
}
