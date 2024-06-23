using ExchangeBook.DTO.AuthorDTOs;
using System.ComponentModel.DataAnnotations;

namespace ExchangeBook.DTO.BookDTOs
{
    public class BookInsertDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title should be between 2 and 50 characters.")]
        public string? Title { get; set; }

        public AuthorInsertDTO? Author { get; set; }
    }
}
