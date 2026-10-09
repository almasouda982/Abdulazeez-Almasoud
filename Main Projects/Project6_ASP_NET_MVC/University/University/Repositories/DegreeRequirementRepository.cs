using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Repositories
{
    public class DegreeRequirementRepository : IDegreeRequirementRepository
    {
        private readonly AppDbContext _db;

        public DegreeRequirementRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DegreeRequirement> GetAll()
        {
            return _db.DegreeRequirements
                .Include(d => d.DegreeProgram)
                .ToList();
        }

        public DegreeRequirement? GetDegreeRequirementByUuid(string uuid)
        {
            return _db.DegreeRequirements
                .Include(d => d.DegreeProgram)
                .FirstOrDefault(d => d.Uuid == uuid);
        }

        public void AddDegreeRequirement(DegreeRequirement degreeRequirement)
        {
            _db.DegreeRequirements.Add(degreeRequirement);
            _db.SaveChanges();
        }

        public void UpdateDegreeRequirement(DegreeRequirement degreeRequirement)
        {
            _db.SaveChanges();
        }

        public void DeleteDegreeRequirement(string uuid)
        {
            var degreeRequirement = _db.DegreeRequirements.FirstOrDefault(d => d.Uuid == uuid);
            if (degreeRequirement != null)
            {
                _db.DegreeRequirements.Remove(degreeRequirement);
                _db.SaveChanges();
            }
        }

        public IEnumerable<DegreeProgram> GetDegreePrograms()
        {
            return _db.DegreePrograms.ToList();
        }
    }
}
