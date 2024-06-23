using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;

namespace ExchangeBook.Services
{
    public interface IBookService
    {
        Task<Book?> CreateBookAsync( BookInsertDTO dto);
        Task<Book?> GetBookWithAuthorById(int id);


    }
}
