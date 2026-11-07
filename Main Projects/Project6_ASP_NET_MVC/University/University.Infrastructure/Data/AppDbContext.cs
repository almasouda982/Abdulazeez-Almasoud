using University.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Data
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
        public DbSet<Book> Books { get; set; }
        public DbSet<CourseBook> CourseBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseBook>()
                .HasOne(cb => cb.Course)
                .WithMany(c => c.CourseBooks)
                .HasForeignKey(cb => cb.CourseId);

            modelBuilder.Entity<CourseBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.CourseBooks)
                .HasForeignKey(cb => cb.BookId);
        }
    }
}
