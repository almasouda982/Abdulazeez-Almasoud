using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _db;

        public BookRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetAll()
        {
            return _db.Books
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();
        }

        public Book? GetBookByUuid(string uuid)
        {
            return _db.Books
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefault(e => e.Uuid == uuid);
        }

        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _db.SaveChanges();
        }

        public void DeleteBook(string uuid)
        {
            var book = _db.Books.FirstOrDefault(b => b.Uuid == uuid);
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _db.Students.ToList();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _db.Courses.ToList();
        }

    }
}
