namespace ExchangeBook.Data
{
    public class Book
    {
        public Book()
        {
        }

        public Book(int id, string? title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        //public string Description { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        public virtual ICollection<Person> Persons { get; set; } = new HashSet<Person>();
        public virtual ICollection<StoreBook> StoreBooks { get; set;} = new HashSet<StoreBook>();
        public virtual ICollection<Notification> Notifications { get; } = new HashSet<Notification>();
    }
}
