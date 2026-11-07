
using Microsoft.AspNetCore.Http;
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
                Author = b.Author,
                Price = b.Price,
                ImageUrl = b.ImageUrl
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
                Author = book.Author,
                Price = book.Price,
                ImageUrl = book.ImageUrl
            };
        }

        public void AddBook(BookCreateDto bookCreateDto)
        {
            string? imageUrl = null;

            if (bookCreateDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(bookCreateDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only image files are accepted.");
                }
                imageUrl = UploadImage(bookCreateDto.ImageFile);
            }

            var book = new Book
            {
                Title = bookCreateDto.Title,
                Author = bookCreateDto.Author,
                Price = bookCreateDto.Price,
                ImageUrl = imageUrl
            };
            _unitOfWork.BookRepository.AddBook(book);
        }

        public void UpdateBook(BookUpdateDto bookDto)
        {
            var oldbook = _unitOfWork.BookRepository.GetBookByUuid(bookDto.Uuid);
            if (oldbook == null)
            {
                throw new KeyNotFoundException($"Book with UUID not found.");
            }

            if (bookDto.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(bookDto.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Only image files are accepted.");
                }
                oldbook.ImageUrl = UploadImage(bookDto.ImageFile);
            }

            oldbook.Title = bookDto.Title;
            oldbook.Author = bookDto.Author;
            oldbook.Price = bookDto.Price;

            _unitOfWork.BookRepository.UpdateBook(oldbook);
        }

        public void DeleteBook(string uuid)
        {
            var book = _unitOfWork.BookRepository.GetBookByUuid(uuid);
            if (book != null)
            {
                _unitOfWork.BookRepository.DeleteBook(uuid);
            }
        }


        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "books"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/books/" + fileName;
        }
    }
}
