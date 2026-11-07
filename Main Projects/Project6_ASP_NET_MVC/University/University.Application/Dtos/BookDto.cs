using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Application.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } 
    }
    public class BookCreateDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public IFormFile? ImageFile { get; set; } 
    }
    public class BookUpdateDto : BookCreateDto
    {
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public string? ImageUrl { get; set; } 
    }
}
