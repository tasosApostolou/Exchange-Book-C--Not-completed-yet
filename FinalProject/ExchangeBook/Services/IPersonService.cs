using ExchangeBook.Data;

namespace ExchangeBook.Services
{
    public interface IPersonService
    {
        Task AddBookToPersonAsync(int personId, int BookId);
        Task<List<Book>> GetPersonBooksAsync(int? id);
        Task<bool> DeletePersonBookAsync(int personId, int bookId);



    }
}
