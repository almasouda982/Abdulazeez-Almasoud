namespace University.Repositories.Base
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IWaitlistEntryRepository WaitlistEntryRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        ICoursePrerequisiteRepository CoursePrerequisiteRepository { get; }
        ICourseSectionRepository CourseSectionRepository { get; }
        IDegreeRequirementRepository DegreeRequirementRepository { get; }
        IDegreeProgramRepository DegreeProgramRepository { get; }
    }
}
