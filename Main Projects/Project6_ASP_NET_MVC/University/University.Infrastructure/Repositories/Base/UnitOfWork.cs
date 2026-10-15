using University.Data;

namespace University.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            StudentRepository = new StudentRepository(_db);
            CourseRepository = new CourseRepository(_db);
            WaitlistEntryRepository = new WaitlistEntryRepository(_db);
            EnrollmentRepository = new EnrollmentRepository(_db);
            CoursePrerequisiteRepository = new CoursePrerequisiteRepository(_db);
            CourseSectionRepository = new CourseSectionRepository(_db);
            DegreeRequirementRepository = new DegreeRequirementRepository(_db);
            DegreeProgramRepository = new DegreeProgramRepository(_db);
            
        }
        public IStudentRepository StudentRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public IWaitlistEntryRepository WaitlistEntryRepository { get; }
        public IEnrollmentRepository EnrollmentRepository { get; }
        public ICoursePrerequisiteRepository CoursePrerequisiteRepository { get; }
        public ICourseSectionRepository CourseSectionRepository { get; }
        public IDegreeRequirementRepository DegreeRequirementRepository { get; }
        public IDegreeProgramRepository DegreeProgramRepository { get; }
    }
}
