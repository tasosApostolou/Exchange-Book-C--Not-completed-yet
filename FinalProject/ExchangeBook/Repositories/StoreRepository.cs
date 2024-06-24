using ExchangeBook.Data;
using ExchangeBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(BookExchangeDbContext context) : base(context)
        {
        }
            public async Task<List<User>> GetAllUsersStoresAsync()
        {
            var usersWithStoreRole = await _context.Users
                .Where(u => u.UserRole == models.UserRole.STORE)
                .Include(u => u.Store) .ToListAsync();
            return usersWithStoreRole;
        }
        public async Task<Store> GetStoreIncludeBooksAsync(int id)
        {
            var store = _context.Stores
                .Where(s => s.Id == id)
                .Include(s => s.StoreBooks)
                .ThenInclude(sb => sb.Book)
                .ThenInclude(b => b.Author)
                .FirstOrDefault();

            return store;
        }
         public async Task<List<StoreBook>> GetStoreBooksByStoreIdAsyng(int storeId)
        {
            List<StoreBook> storeBooks;
            storeBooks = await _context.Stores
                .Where(s => s.Id == storeId)
                .SelectMany(s => s.StoreBooks)
                .Include(sb => sb.Book)
                .ThenInclude(b => b.Author)
                .ToListAsync();
            return storeBooks;
        }

    }
}