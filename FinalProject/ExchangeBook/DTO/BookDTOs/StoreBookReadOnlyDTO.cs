using System.ComponentModel.DataAnnotations;

namespace ExchangeBook.DTO.BookDTOs
{
    public class StoreBookReadOnlyDTO
    {
        public BookReadOnlyDTO? Book { get; set; }
        public double? Price { get; set; }
    }
}
