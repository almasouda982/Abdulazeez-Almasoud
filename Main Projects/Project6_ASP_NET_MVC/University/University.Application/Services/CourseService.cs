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
                BookId = c.BookId,
                BookTitle = c.Book != null ? c.Book.Title : null,
                BookImageUrl = c.Book?.ImageUrl

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
                Description = crs.Description,
                BookId = crs.BookId,
                
            };
        }

        public void AddCourse(CourseCreateDto courseCreateDto)
        {
            var course = new Course
            {
                Code = courseCreateDto.Code,
                Title = courseCreateDto.Title,
                Credits = courseCreateDto.Credits,
                Description = courseCreateDto.Description,
                BookId = courseCreateDto.BookId
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
            oldcrs.BookId = courseUpdateDto.BookId;

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

    }
}
