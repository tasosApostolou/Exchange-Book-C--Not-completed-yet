using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public interface IBookRepository
    {
        Task<List<Person>> GetBookPersonsAsync(int id);
        Task<List<Book>> GetBooksByTitle(string? title);
        Task<Book> GetBookWithAuthorByIdAsync(int bookId);

    }
}
