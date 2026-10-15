using University.Dtos;
using University.Models;

namespace University.Services.Base
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAllStudents();
        StudentUpdateDto? GetStudentByUuid(string uuid);
        void AddStudent(StudentCreateDto studentCreateDto);
        void UpdateStudent(StudentUpdateDto studentUpdateDto);
        void DeleteStudent(string uuid);
        IEnumerable<DegreeProgram> GetDegreePrograms();
    }
}
