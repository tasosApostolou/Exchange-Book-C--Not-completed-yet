using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Book>> GetPersonBooksAsync(int? id);
        Task<Person?> GetByPhoneNumber(string? phoneNumber);
        Task<List<User>> GetAllUsersPersonsAsync();
        Task AddBookToPersonAsync(int personId, int bookId);
        Task<bool> DeletePersonBookAsync(int? personId, int? bookId);//Delete book from person, delete rec from Person_book (many-many)
        Task<Person> GetPersonWithBooksAsync(int? id);


    }
}
