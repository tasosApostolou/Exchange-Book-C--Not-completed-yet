using ExchangeBook.models;

namespace ExchangeBook.DTO.UserDTOs
{
    public class UserReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
