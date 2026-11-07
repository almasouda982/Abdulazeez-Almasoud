using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Application.Dtos
{
    public class CourseBookDto
    {
        public int Id { get; set; } 
        public int CourseId { get; set; }
        public int BookId { get; set; }
        public BookDto Book { get; set; } = null!; 
    }
}
