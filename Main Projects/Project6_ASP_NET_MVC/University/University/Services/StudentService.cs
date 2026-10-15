using University.Dtos;
using University.Models;
using University.Repositories.Base;
using University.Services.Base;

namespace University.Services
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
                EnrollmentStatus = s.EnrollmentStatus
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
                EnrollmentStatus = std.EnrollmentStatus
            };
        }

        public void AddStudent(StudentCreateDto studentCreateDto)
        {
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
                EnrollmentStatus = studentCreateDto.EnrollmentStatus
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
    }
}
