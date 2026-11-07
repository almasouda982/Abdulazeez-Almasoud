
using University.Application.Dtos;
using University.Application.Services.Base;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;

namespace University.Application.Services
{
    public class CourseBookService : ICourseBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CourseBookDto> GetBooksForCourse(int courseId)
        {
            var courseBooks = _unitOfWork.CourseBookRepository.GetBooksForCourse(courseId);

            return courseBooks.Select(cb => new CourseBookDto
            {
                Id = cb.Id,
                CourseId = cb.CourseId,
                BookId = cb.BookId,
                Book = new BookDto
                {
                    Id = cb.Book.Id,
                    Uuid = cb.Book.Uuid,
                    Title = cb.Book.Title,
                    Author = cb.Book.Author,
                    Price = cb.Book.Price,
                    ImageUrl = cb.Book.ImageUrl
                }
            }).ToList();
        }

        public void AddBookToCourse(AssignBookToCourseRequest request)
        {
            var courseBook = new CourseBook
            {
                CourseId = request.CourseId,
                BookId = request.BookId
            };

            _unitOfWork.CourseBookRepository.AddBookToCourse(courseBook);
        }

        public void RemoveBookFromCourse(int courseId, int bookId)
        {
            _unitOfWork.CourseBookRepository.RemoveBookFromCourse(courseId, bookId);
        }
    }
}
