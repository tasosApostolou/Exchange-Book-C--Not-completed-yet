using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Repositories;
using ExchangeBook.Services.Exceptions;

namespace ExchangeBook.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public PersonService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddBookToPersonAsync(int personId, int bookId)
        {
            Book? book;
            Person? person;
            try
            {
                person = await _unitOfWork.PersonRepository.GetAsync(personId);
                if(person == null)
                {
                    throw new Exception();
                }
                book  = await _unitOfWork.BookRepository.GetAsync(bookId);
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }
                person.Books.Add(book);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }

        public async Task<List<Book>> GetPersonBooksAsync(int? id)
        {
            List<Book> books = new();

            try
            {
                books = await _unitOfWork!.PersonRepository.GetPersonBooksAsync(id);
                _logger!.LogInformation("{Message}", "Student count retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return books;
        }

        public async Task<bool> DeletePersonBookAsync(int personId, int bookId)
        {
            bool deleted = false;
            Person person;
            Book book;

            try
            {
                if ( await _unitOfWork.PersonRepository.GetAsync(personId) == null) throw new PersonNotFoundException("Person not found");
                if (await _unitOfWork.BookRepository.GetAsync(bookId) == null) throw new BookNotFoundException("Book Not Found");
                deleted = await _unitOfWork.PersonRepository.DeletePersonBookAsync(personId, bookId);
                
            }catch(Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return deleted;
        }
    }
}
