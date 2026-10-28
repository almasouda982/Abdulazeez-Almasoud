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
                ImageUrl = c.ImageUrl,
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
                ImageUrl = crs.ImageUrl,
            };
        }

        public void AddCourse(CourseCreateDto courseCreateDto)
        {
            string? imageUrl = null;

            if (courseCreateDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(courseCreateDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only image files are accepted.");
                }
                imageUrl = UploadImage(courseCreateDto.ImageFile);
            }

            var course = new Course
            {
                Code = courseCreateDto.Code,
                Title = courseCreateDto.Title,
                Credits = courseCreateDto.Credits,
                Description = courseCreateDto.Description,
                ImageUrl= imageUrl,
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

            if (courseUpdateDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(courseUpdateDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only image files are accepted.");
                }
                oldcrs.ImageUrl = UploadImage(courseUpdateDto.ImageFile);
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

        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "courses"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/courses/" + fileName;
        }
    }
}
