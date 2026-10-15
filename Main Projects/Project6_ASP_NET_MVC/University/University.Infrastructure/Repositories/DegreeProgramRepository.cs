using University.Data;
using University.Models;

namespace University.Repositories
{
    public class DegreeProgramRepository : IDegreeProgramRepository
    {
        private readonly AppDbContext _db;

        public DegreeProgramRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DegreeProgram> GetAll()
        {
            return _db.DegreePrograms.ToList();
        }

        public DegreeProgram? GetDegreeProgramByUuid(string uuid)
        {
            return _db.DegreePrograms.FirstOrDefault(d => d.Uuid == uuid);
        }

        public void AddDegreeProgram(DegreeProgram degreeProgram)
        {
            _db.DegreePrograms.Add(degreeProgram);
            _db.SaveChanges();
        }

        public void UpdateDegreeProgram(DegreeProgram degreeProgram)
        {
            _db.SaveChanges();
        }

        public void DeleteDegreeProgram(string uuid)
        {
            var degreeProgram = _db.DegreePrograms.FirstOrDefault(d => d.Uuid == uuid);
            if (degreeProgram != null)
            {
                _db.DegreePrograms.Remove(degreeProgram);
                _db.SaveChanges();
            }
        }
    }
}
