using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public interface IStoreRepository
    {
        Task<List<User>> GetAllUsersStoresAsync();

    }
}
