
using University.Application.Dtos;
using University.Application.Services.Base;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;

namespace University.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _unitOfWork.BookRepository.GetAll();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Uuid = b.Uuid,
                Title = b.Title,
                LastName = b.Student != null ? b.Student.LastName : null,
                Author = b.Author,
                Price = b.Price,
                Code = b.Course != null ? b.Course.Code : null,
            });
        }


        public BookUpdateDto? GetBookByUuid(string uuid)
        {
            var book = _unitOfWork.BookRepository.GetBookByUuid(uuid);
            if (book == null) return null;
            return new BookUpdateDto
            {
                Uuid = book.Uuid,
                Title = book.Title,
                StudentId = book.StudentId,
                Author = book.Author,
                Price = book.Price,
                CourseId = book.CourseId,
            };
        }

        public void AddBook(BookCreateDto bookCreateDto)
        {
            var book = new Book
            {
                Title = bookCreateDto.Title,
                StudentId = bookCreateDto.StudentId,
                Author = bookCreateDto.Author,
                Price = bookCreateDto.Price,
                CourseId = bookCreateDto.CourseId,
            };
            _unitOfWork.BookRepository.AddBook(book);
        }


        public void UpdateBook(BookUpdateDto bookDto)
        {
            var oldbook = _unitOfWork.BookRepository.GetBookByUuid(bookDto.Uuid);
            if (oldbook != null)
            {
                throw new KeyNotFoundException($"Book with UUID not found.");

            }

                oldbook.Title = bookDto.Title;
                oldbook.StudentId = bookDto.StudentId;
                oldbook.Author = bookDto.Author;
                oldbook.Price = bookDto.Price;
                oldbook.CourseId = bookDto.CourseId;
                _unitOfWork.BookRepository.UpdateBook(oldbook);
            
        }

        public void DeleteBook(string uuid)
        {
            var book = _unitOfWork.BookRepository.GetBookByUuid(uuid);
            if (book == null)
            {
                _unitOfWork.BookRepository.DeleteBook(uuid);
            }

        }


        public IEnumerable<Student> GetStudents()
        {
            return _unitOfWork.BookRepository.GetStudents();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _unitOfWork.BookRepository.GetCourses();
        }





    }
}
