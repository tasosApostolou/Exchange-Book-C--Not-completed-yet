namespace ExchangeBook.Services.Exceptions
{
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException(string? message) : base(message)
        {
        }
    }
}
