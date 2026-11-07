using Microsoft.EntityFrameworkCore;
using University.Domain.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class CourseBookRepository : ICourseBookRepository
    {
        private readonly AppDbContext _db;

        public CourseBookRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<CourseBook> GetBooksForCourse(int courseId)
        {
            return _db.CourseBooks
                .Where(cb => cb.CourseId == courseId)
                .Include(cb => cb.Book)
                .ToList();
        }

        public void AddBookToCourse(CourseBook courseBook)
        {
            var exists = _db.CourseBooks.Any(cb => cb.CourseId == courseBook.CourseId && cb.BookId == courseBook.BookId);
            if (!exists)
            {
                _db.CourseBooks.Add(courseBook);
                _db.SaveChanges();
            }
        }

        public void RemoveBookFromCourse(int courseId, int bookId)
        {
            var courseBook = _db.CourseBooks
                .FirstOrDefault(cb => cb.CourseId == courseId && cb.BookId == bookId);

            if (courseBook != null)
            {
                _db.CourseBooks.Remove(courseBook);
                _db.SaveChanges();
            }
        }

        public async Task<CourseBook?> GetByCourseAndBookIdAsync(int courseId, int bookId)
        {
            return await _db.CourseBooks
                .FirstOrDefaultAsync(cb => cb.CourseId == courseId && cb.BookId == bookId);
        }
    }
}
