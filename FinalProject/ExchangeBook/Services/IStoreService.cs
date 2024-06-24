using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;

namespace ExchangeBook.Services
{
    public interface IStoreService
    {
        Task AddBookToStoreAsync(int storeId, double price, Book? book);
    }
}
