using ExchangeBook.models;

namespace ExchangeBook.Data
{
    public class Notification
    {
        public int Id { get; set; }

        public int? InterestedUserId { get; set; }
        public virtual User? User { get; set; }

        public int? UserId { get; set; }
        public virtual User? HolderUser { get; set; }

        public int? BookId { get; set; }
        public virtual Book? Book { get; set; }

        public NotificationType? Type { get; set; }
        public bool? IsSeen { get; set; }
    }
}
