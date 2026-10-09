using University.Models;

namespace University.Repositories
{
    public interface IDegreeRequirementRepository
    {
        IEnumerable<DegreeRequirement> GetAll();
        DegreeRequirement? GetDegreeRequirementByUuid(string uuid);
        void AddDegreeRequirement(DegreeRequirement degreeRequirement);
        void UpdateDegreeRequirement(DegreeRequirement degreeRequirement);
        void DeleteDegreeRequirement(string uuid);
        IEnumerable<DegreeProgram> GetDegreePrograms();
    }
}
