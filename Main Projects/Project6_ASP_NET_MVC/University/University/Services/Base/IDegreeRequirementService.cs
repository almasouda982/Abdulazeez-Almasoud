using University.Dtos;
using University.Models;

namespace University.Services.Base
{
    public interface IDegreeRequirementService
    {
        IEnumerable<DegreeRequirementDto> GetAllDegreeRequirements();
        DegreeRequirementUpdateDto? GetDegreeRequirementByUuid(string uuid);
        void AddDegreeRequirement(DegreeRequirementCreateDto degreeRequirementCreateDto);
        void UpdateDegreeRequirement(DegreeRequirementUpdateDto degreeRequirementUpdateDto);
        void DeleteDegreeRequirement(string uuid);
        IEnumerable<DegreeProgram> GetDegreePrograms();
    }
}
