namespace ExchangeBook.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        PersonService PersonService { get; }
        StoreService StoreService { get; }
        BookService BookService { get; }
       
 
    }
}
