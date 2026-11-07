using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Dtos;

namespace University.Application.Services.Base
{
    public interface ICourseBookService
    {
        IEnumerable<CourseBookDto> GetBooksForCourse(int courseId);
        void AddBookToCourse(AssignBookToCourseRequest request);
        void RemoveBookFromCourse(int courseId, int bookId);
    }
}
