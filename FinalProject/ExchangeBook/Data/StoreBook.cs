namespace ExchangeBook.Data
{
    public class StoreBook
    {
        public int? StoreId { get; set; }
        public Store? Store { get; set; }

        public int? BookId { get; set; }
        public Book? Book { get; set; }

        public double? Price { get; set; }
    }
}
