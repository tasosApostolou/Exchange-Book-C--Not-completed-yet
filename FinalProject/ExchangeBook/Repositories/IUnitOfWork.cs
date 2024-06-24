namespace ExchangeBook.Repositories
{
    public interface IUnitOfWork
    {
        public BookRepository BookRepository { get; }
        public PersonRepository PersonRepository { get; }
        public StoreRepository StoreRepository { get; }
        public StoreBookRepository StoreBookRepository { get; }
        public UserRepositorty UserRepositorty { get; }
        public AuthorRepository AuthorRepository { get; }

        Task<bool> SaveAsync();
    }
}
