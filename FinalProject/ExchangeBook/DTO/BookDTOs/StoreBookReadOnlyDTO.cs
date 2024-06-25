using ExchangeBook.DTO.StoreDTO;
using System.ComponentModel.DataAnnotations;

namespace ExchangeBook.DTO.BookDTOs
{
    public class StoreBookReadOnlyDTO
    {
        public StoreReadOnlyDTO Store { get; set; }
        public BookReadOnlyDTO? Book { get; set; }
        public double? Price { get; set; }
    }
}
