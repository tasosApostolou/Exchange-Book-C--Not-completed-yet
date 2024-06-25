using ExchangeBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class StoreBookRepository : BaseRepository<StoreBook>, IStoreBookRepository
    {
        public StoreBookRepository(BookExchangeDbContext contex) : base(contex)
        {
        }

        public async Task<List<StoreBook>> GetByBookTitleAsync(string? title)
        {
            List<StoreBook> storeBooks;
            storeBooks = await _context.StoreBooks
                .Where(sb => sb.Book.Title.StartsWith(title))
                .Include(sb => sb.Store)
                .Include(sb => sb.Book)
                .ThenInclude(b => b.Author)
                //.Where(sb => sb.Book.Title.StartsWith(title))
                .ToListAsync();
            return storeBooks;
        }

        public async Task<List<StoreBook>> GetStoreBooksByStoreIdAsync(int storeId)
        {
            var storeBooks = await _context.StoreBooks
                .Include(sb => sb.Book)
                .ThenInclude(b => b.Author)
                .Where(sb => sb.StoreId == storeId)
                .ToListAsync();

            return storeBooks;
        }

        public async Task<bool> RemoveBookFromStoreAsync(int storeId, int bookId)
        {
            var storeBook = await _context.StoreBooks
                .FirstOrDefaultAsync(sb => sb.StoreId == storeId && sb.BookId == bookId);

            if (storeBook != null)
            {
                 _context.StoreBooks.Remove(storeBook);
                return true;
            }
            return false;
        }
    }
}
