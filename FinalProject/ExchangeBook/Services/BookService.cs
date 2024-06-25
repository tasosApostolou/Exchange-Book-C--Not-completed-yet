using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public BookService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Book> CreateBookAsync(BookInsertDTO? dto)
        {
            Book? book;
            try
            {

                book = _mapper.Map<Book>(dto); // Configuration also AuthorInsertDTO (property of BookInsertDTO) convert to model Author
                var existedAuthor = await _unitOfWork.AuthorRepository.GetAuthorByName(dto.Author.Name);
                if (existedAuthor != null)
                {
                    book.Author = existedAuthor;
                }
                await _unitOfWork.BookRepository.AddAsync(book);
                await _unitOfWork.SaveAsync();
                return book;

            }
            catch (Exception e)
            {
               Console.WriteLine(e.StackTrace);
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
        public async Task<Book> CreateBookStoreAsync(BookInsertDTO? dto)
        {
            Book? book;
            try
            {

                book = _mapper.Map<Book>(dto); // Configuration also AuthorInsertDTO (property of BookInsertDTO) convert to model Author
                var existedAuthor = await _unitOfWork.AuthorRepository.GetAuthorByName(dto.Author.Name);
                if (existedAuthor != null)
                {
                    book.Author = existedAuthor;
                }
                await _unitOfWork.BookRepository.AddAsync(book);
                await _unitOfWork.SaveAsync();
                return book;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
        public async Task<Book?> GetBookById(int id)
        {
            Book? book;
            try
            {
                book = await _unitOfWork!.BookRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "Book with id: " + id + " Success");
                Console.WriteLine(book.Author.Name + " author adasd dsdasd ");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return book;
        }
        public async Task<Book?> GetBookWithAuthorById(int id)
        {
            Book? book;
            try
            {
                book = await _unitOfWork!.BookRepository.GetBookWithAuthorByIdAsync(id);
                _logger!.LogInformation("{Message}", "Book with id: " + id + " Success");
                Console.WriteLine(book.Author.Name + " author adasd dsdasd ");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return book;
        }
        public async Task<List<Book>> GetBooksByTitleAsync(string? title)
        {
            List<Book> books = new();

            try
            {
                books = await _unitOfWork!.BookRepository.GetBooksByTitle(title);
                _logger!.LogInformation("{Message}", "Student count retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return books;
        }
    }
}
