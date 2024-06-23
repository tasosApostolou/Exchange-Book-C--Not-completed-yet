namespace ExchangeBook.Services.Exceptions
{
    public class PersonAlreadyExistsException : Exception
    {
        public PersonAlreadyExistsException(string? message) : base(message)
        {
        }   
    }
}
