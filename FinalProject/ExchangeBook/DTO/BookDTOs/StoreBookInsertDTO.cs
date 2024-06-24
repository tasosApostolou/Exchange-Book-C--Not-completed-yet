using System.ComponentModel.DataAnnotations;

namespace ExchangeBook.DTO.BookDTOs
{
    public class StoreBookInsertDTO
    {
        public BookInsertDTO? Book { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double Price { get; set; }

    }
}
