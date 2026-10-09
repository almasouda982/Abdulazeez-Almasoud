using University.Models;

namespace University.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student? GetStudentByUuid(string uuid);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(string uuid);
        IEnumerable<DegreeProgram> GetDegreePrograms();
    }
}
