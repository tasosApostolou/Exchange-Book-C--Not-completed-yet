using ExchangeBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class StoreBookRepository : BaseRepository<StoreBook>, IStoreBookRepository
    {
        public StoreBookRepository(BookExchangeDbContext contex) : base(contex)
        {
        }

        public async Task<List<StoreBook>> GetByBookTitle(string? title)
        {
            List<StoreBook> storeBooks;
            storeBooks = await _context.StoreBooks
                .Where(sb => sb.Book.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            return storeBooks;
        }


    }
}
