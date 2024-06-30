using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.DTO.PersonDTO;

namespace ExchangeBook.Services
{
    public interface IPersonService
    {
        Task AddBookToPersonAsync(int personId, int BookId);
        Task<List<Book>> GetPersonBooksAsync(int? id);
        Task<Person?> UpdatePersonAsync(int personId, PersonDTO personDTO);
        Task<BookReadOnlyDTO?> DeletePersonBookAsync(int personId, int bookId);




    }
}
