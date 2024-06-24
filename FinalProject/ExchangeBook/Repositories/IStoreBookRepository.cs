using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public interface IStoreBookRepository
    {
        Task<List<StoreBook>> GetByBookTitle(string? title);
        Task<List<StoreBook>> GetStoreBooksByStoreIdAsync(int storeId);

    }
}
