using Microsoft.AspNetCore.Http;
using University.Application.Dtos;
using University.Application.Services.Base;
using University.Domain.Models;
using University.Infrastructure.Repositories.Base;

namespace University.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CourseDto> GetAllCourses()
        {
            var courses = _unitOfWork.CourseRepository.GetAll();

            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Uuid = c.Uuid,
                Code = c.Code,
                Title = c.Title,
                Credits = c.Credits,
                Description = c.Description,
                // Map the collection of stacked books through CourseBooks
                Books = c.CourseBooks != null ? c.CourseBooks.Select(cb => new BookDto
                {
                    Id = cb.Book.Id,
                    Uuid = cb.Book.Uuid,
                    Title = cb.Book.Title,
                    Author = cb.Book.Author,
                    Price = cb.Book.Price,
                    ImageUrl = cb.Book.ImageUrl
                }).ToList() : new List<BookDto>()
            }).ToList();
        }

        public CourseUpdateDto? GetCourseByUuid(string uuid)
        {
            var crs = _unitOfWork.CourseRepository.GetCourseByUuid(uuid);
            if (crs == null)
            {
                return null;
            }

            return new CourseUpdateDto
            {
                Uuid = crs.Uuid,
                Code = crs.Code,
                Title = crs.Title,
                Credits = crs.Credits,
                Description = crs.Description
            };
        }

        public void AddCourse(CourseCreateDto courseCreateDto)
        {
            var course = new Course
            {
                Code = courseCreateDto.Code,
                Title = courseCreateDto.Title,
                Credits = courseCreateDto.Credits,
                Description = courseCreateDto.Description
            };

            _unitOfWork.CourseRepository.AddCourse(course);
        }

        public void UpdateCourse(CourseUpdateDto courseUpdateDto)
        {
            var oldcrs = _unitOfWork.CourseRepository.GetCourseByUuid(courseUpdateDto.Uuid);
            if (oldcrs == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            oldcrs.Code = courseUpdateDto.Code;
            oldcrs.Title = courseUpdateDto.Title;
            oldcrs.Credits = courseUpdateDto.Credits;
            oldcrs.Description = courseUpdateDto.Description;

            _unitOfWork.CourseRepository.UpdateCourse(oldcrs);
        }

        public void DeleteCourse(string uuid)
        {
            var crs = _unitOfWork.CourseRepository.GetCourseByUuid(uuid);
            if (crs != null)
            {
                _unitOfWork.CourseRepository.DeleteCourse(uuid);
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            return _unitOfWork.CourseRepository.GetBooks();
        }

        public void AssignBookToCourse(AssignBookToCourseRequest request)
        {
            _unitOfWork.CourseRepository.AddBookToCourse(request.CourseId, request.BookId);
        }

        public void RemoveBookFromCourse(int courseId, int bookId)
        {
            _unitOfWork.CourseRepository.RemoveBookFromCourse(courseId, bookId);
        }
    }
}
