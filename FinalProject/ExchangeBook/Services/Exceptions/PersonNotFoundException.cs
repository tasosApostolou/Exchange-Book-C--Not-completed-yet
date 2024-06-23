namespace ExchangeBook.Services.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(string? message) : base(message)
        {
        }
    }
}
