
using University.Application.Dtos;
using University.Domain.Models;

namespace University.Application.Services.Base
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks();
        BookUpdateDto? GetBookByUuid(string uuid);
        void AddBook(BookCreateDto bookDto);
        void UpdateBook(BookUpdateDto bookDto);
        void DeleteBook(string uuid);
    }
}
