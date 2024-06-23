using ExchangeBook.models;

namespace ExchangeBook.DTO.UserDTOs
{
    public class UserStoreReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? storeId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
