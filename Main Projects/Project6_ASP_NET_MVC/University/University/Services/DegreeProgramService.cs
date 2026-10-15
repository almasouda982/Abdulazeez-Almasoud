using University.Dtos;
using University.Models;
using University.Repositories.Base;
using University.Services.Base;

namespace University.Services
{
    public class DegreeProgramService : IDegreeProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DegreeProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DegreeProgramDto> GetAllDegreePrograms()
        {
            var degreePrograms = _unitOfWork.DegreeProgramRepository.GetAll();

            return degreePrograms.Select(dp => new DegreeProgramDto
            {
                Id = dp.Id,
                Uuid = dp.Uuid,
                Name = dp.Name,
                CatalogYear = dp.CatalogYear,
                TotalCreditsRequired = dp.TotalCreditsRequired
            }).ToList();
        }

        public DegreeProgramUpdateDto? GetDegreeProgramByUuid(string uuid)
        {
            var degp = _unitOfWork.DegreeProgramRepository.GetDegreeProgramByUuid(uuid);
            if (degp == null)
            {
                return null;
            }

            return new DegreeProgramUpdateDto
            {
                Uuid = degp.Uuid,
                Name = degp.Name,
                CatalogYear = degp.CatalogYear,
                TotalCreditsRequired = degp.TotalCreditsRequired
            };
        }

        public void AddDegreeProgram(DegreeProgramCreateDto degreeProgramCreateDto)
        {
            var degreeProgram = new DegreeProgram
            {
                Name = degreeProgramCreateDto.Name,
                CatalogYear = degreeProgramCreateDto.CatalogYear,
                TotalCreditsRequired = degreeProgramCreateDto.TotalCreditsRequired
            };

            _unitOfWork.DegreeProgramRepository.AddDegreeProgram(degreeProgram);
        }

        public void UpdateDegreeProgram(DegreeProgramUpdateDto degreeProgramUpdateDto)
        {
            var olddegp = _unitOfWork.DegreeProgramRepository.GetDegreeProgramByUuid(degreeProgramUpdateDto.Uuid);
            if (olddegp == null)
            {
                throw new KeyNotFoundException("Degree program not found");
            }

            olddegp.Name = degreeProgramUpdateDto.Name;
            olddegp.CatalogYear = degreeProgramUpdateDto.CatalogYear;
            olddegp.TotalCreditsRequired = degreeProgramUpdateDto.TotalCreditsRequired;

            _unitOfWork.DegreeProgramRepository.UpdateDegreeProgram(olddegp);
        }

        public void DeleteDegreeProgram(string uuid)
        {
            var degp = _unitOfWork.DegreeProgramRepository.GetDegreeProgramByUuid(uuid);
            if (degp != null)
            {
                _unitOfWork.DegreeProgramRepository.DeleteDegreeProgram(uuid);
            }
        }
    }
}
