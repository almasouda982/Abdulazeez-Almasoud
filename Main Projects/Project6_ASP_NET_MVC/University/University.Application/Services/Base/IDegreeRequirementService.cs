using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
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
