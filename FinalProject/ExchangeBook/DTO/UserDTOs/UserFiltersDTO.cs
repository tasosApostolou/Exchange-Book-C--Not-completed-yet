using ExchangeBook.models;

namespace ExchangeBook.DTO.UserDTOs
{
    public class UserFiltersDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
