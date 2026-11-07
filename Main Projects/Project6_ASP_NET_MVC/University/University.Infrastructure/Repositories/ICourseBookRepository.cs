using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Models;

namespace University.Infrastructure.Repositories
{
    public interface ICourseBookRepository
    {
        IEnumerable<CourseBook> GetBooksForCourse(int courseId);
        void AddBookToCourse(CourseBook courseBook);
        void RemoveBookFromCourse(int courseId, int bookId);
        Task<CourseBook?> GetByCourseAndBookIdAsync(int courseId, int bookId);
    }
}