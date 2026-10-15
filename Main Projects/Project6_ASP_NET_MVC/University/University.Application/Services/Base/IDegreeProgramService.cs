using University.Application.Dtos;

namespace University.Application.Services.Base
{
    public interface IDegreeProgramService
    {
        IEnumerable<DegreeProgramDto> GetAllDegreePrograms();
        DegreeProgramUpdateDto? GetDegreeProgramByUuid(string uuid);
        void AddDegreeProgram(DegreeProgramCreateDto degreeProgramCreateDto);
        void UpdateDegreeProgram(DegreeProgramUpdateDto degreeProgramUpdateDto);
        void DeleteDegreeProgram(string uuid);
    }
}
