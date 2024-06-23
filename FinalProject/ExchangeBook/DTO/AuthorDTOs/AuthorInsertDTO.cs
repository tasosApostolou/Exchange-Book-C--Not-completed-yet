using System.ComponentModel.DataAnnotations;

namespace ExchangeBook.DTO.AuthorDTOs
{
    public class AuthorInsertDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "name should be between 2 and 50 characters.")]
        public string? Name { get; set; }
    }
}
