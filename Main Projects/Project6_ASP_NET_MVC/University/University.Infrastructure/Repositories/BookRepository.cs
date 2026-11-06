using Microsoft.EntityFrameworkCore;
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
            return _db.Books.ToList();
        }

        public Book? GetBookByUuid(string uuid)
        {
            return _db.Books.FirstOrDefault(e => e.Uuid == uuid);
        }

        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _db.Books.Update(book);
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
    }
}