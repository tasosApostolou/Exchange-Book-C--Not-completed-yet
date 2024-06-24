
namespace ExchangeBook.Data
{
    public class Store
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }// nullable : αν διαγραφει ο user τοτε το ξενο κλειδι θα γινει null διαφορετικα θα διαγραφει και το store 
        public virtual ICollection<StoreBook> StoreBooks { get;} = new HashSet<StoreBook>();

    }
}
