using ExchangeBook.models;

namespace ExchangeBook.DTO.UserDTOs
{
    public class UserPersonReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? personId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public UserRole? UserRole { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
