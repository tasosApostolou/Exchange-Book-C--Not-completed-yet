using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;

namespace ExchangeBook.DTO.BookDTOs
{
    public class BookReadOnlyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorReadonlyDTO Author { get; set; }
    }
}
