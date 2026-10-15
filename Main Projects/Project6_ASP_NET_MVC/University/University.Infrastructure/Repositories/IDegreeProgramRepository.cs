using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface IDegreeProgramRepository
    {
        IEnumerable<DegreeProgram> GetAll();
        DegreeProgram? GetDegreeProgramByUuid(string uuid);
        void AddDegreeProgram(DegreeProgram degreeProgram);
        void UpdateDegreeProgram(DegreeProgram degreeProgram);
        void DeleteDegreeProgram(string uuid);
    }
}
