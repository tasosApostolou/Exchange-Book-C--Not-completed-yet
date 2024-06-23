using ExchangeBook.models;

namespace ExchangeBook.Data
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole? UserRole { get; set; }

        public virtual Person? Person { get; set; }
        public virtual Store? Store { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();


        public override string? ToString()
        {
            return $"{Username}, {Id}";
        }
    }
}
