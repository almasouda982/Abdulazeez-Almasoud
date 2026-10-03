using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
        public DbSet<DegreeProgram> DegreePrograms { get; set; }
        public DbSet<DegreeRequirement> DegreeRequirements { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<WaitlistEntry> WaitlistEntries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoursePrerequisite>()
                .HasOne(cp => cp.PrerequisiteCourse)
                .WithMany()
                .HasForeignKey(cp => cp.PrerequisiteCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.LetterGradePoints)
                .HasPrecision(3, 2);
        }
    }
}
