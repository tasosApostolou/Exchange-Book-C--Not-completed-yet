using ExchangeBook.Data;
using ExchangeBook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        private BookExchangeDbContext context;

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

    }
}