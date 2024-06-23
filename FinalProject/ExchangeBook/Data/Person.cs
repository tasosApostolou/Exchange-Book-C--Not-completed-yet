namespace ExchangeBook.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber {  get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Book> Books { get; } = new HashSet<Book>();

        public override string? ToString()
        {
            return $"{Firstname}, {Lastname}, {PhoneNumber}";
        }
    }
}
