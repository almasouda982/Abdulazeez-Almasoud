using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public long StudentId { get; set; }
        public Student? Student { get; set; }

    }
}
