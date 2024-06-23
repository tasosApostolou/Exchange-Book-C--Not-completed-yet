namespace ExchangeBook.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        PersonService PersonService { get; }
        BookService BookService { get; }
       
 
    }
}
