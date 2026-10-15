using University.Dtos;
using University.Models;
using University.Repositories.Base;
using University.Services.Base;

namespace University.Services
{
    public class DegreeRequirementService : IDegreeRequirementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DegreeRequirementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DegreeRequirementDto> GetAllDegreeRequirements()
        {
            var degreeRequirements = _unitOfWork.DegreeRequirementRepository.GetAll();

            return degreeRequirements.Select(d => new DegreeRequirementDto
            {
                Id = d.Id,
                Uuid = d.Uuid,
                Name = d.DegreeProgram != null ? d.DegreeProgram.Name : "",
                CategoryName = d.CategoryName,
                RequiredCredits = d.RequiredCredits,
                MinLevel = d.MinLevel
            }).ToList();
        }

        public DegreeRequirementUpdateDto? GetDegreeRequirementByUuid(string uuid)
        {
            var degr = _unitOfWork.DegreeRequirementRepository.GetDegreeRequirementByUuid(uuid);
            if (degr == null)
            {
                return null;
            }

            return new DegreeRequirementUpdateDto
            {
                Uuid = degr.Uuid,
                DegreeProgramId = degr.DegreeProgramId,
                CategoryName = degr.CategoryName,
                RequiredCredits = degr.RequiredCredits,
                MinLevel = degr.MinLevel
            };
        }

        public void AddDegreeRequirement(DegreeRequirementCreateDto degreeRequirementCreateDto)
        {
            var degreeRequirement = new DegreeRequirement
            {
                DegreeProgramId = degreeRequirementCreateDto.DegreeProgramId,
                CategoryName = degreeRequirementCreateDto.CategoryName,
                RequiredCredits = degreeRequirementCreateDto.RequiredCredits,
                MinLevel = degreeRequirementCreateDto.MinLevel
            };

            _unitOfWork.DegreeRequirementRepository.AddDegreeRequirement(degreeRequirement);
        }

        public void UpdateDegreeRequirement(DegreeRequirementUpdateDto degreeRequirementUpdateDto)
        {
            var olddegr = _unitOfWork.DegreeRequirementRepository.GetDegreeRequirementByUuid(degreeRequirementUpdateDto.Uuid);
            if (olddegr == null)
            {
                throw new KeyNotFoundException("Degree requirement not found");
            }

            olddegr.DegreeProgramId = degreeRequirementUpdateDto.DegreeProgramId;
            olddegr.CategoryName = degreeRequirementUpdateDto.CategoryName;
            olddegr.RequiredCredits = degreeRequirementUpdateDto.RequiredCredits;
            olddegr.MinLevel = degreeRequirementUpdateDto.MinLevel;

            _unitOfWork.DegreeRequirementRepository.UpdateDegreeRequirement(olddegr);
        }

        public void DeleteDegreeRequirement(string uuid)
        {
            var degr = _unitOfWork.DegreeRequirementRepository.GetDegreeRequirementByUuid(uuid);
            if (degr != null)
            {
                _unitOfWork.DegreeRequirementRepository.DeleteDegreeRequirement(uuid);
            }
        }

        public IEnumerable<DegreeProgram> GetDegreePrograms()
        {
            return _unitOfWork.DegreeRequirementRepository.GetDegreePrograms();
        }
    }
}
