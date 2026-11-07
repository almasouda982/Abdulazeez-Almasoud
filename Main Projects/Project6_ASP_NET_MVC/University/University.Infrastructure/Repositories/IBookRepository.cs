using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetBookByUuid(string uuid);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(string uuid);
    }
}
