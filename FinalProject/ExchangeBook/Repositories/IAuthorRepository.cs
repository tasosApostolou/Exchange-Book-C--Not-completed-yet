using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthorsByBookTitleAsync(string title);
        Task<Author> GetAuthorByName(string name);
    }
}
