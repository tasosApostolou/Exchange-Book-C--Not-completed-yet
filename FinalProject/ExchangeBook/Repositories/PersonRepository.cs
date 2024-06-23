using ExchangeBook.Data;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(BookExchangeDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsersPersonsAsync()
        {

            var usersWithPersonalRole = await _context.Users
                   .Where(u => u.UserRole == UserRole.PERSONAL)
                   .Include(u => u.Person)
                   .ToListAsync();
            return usersWithPersonalRole;
        }
    

        public async Task<Person?> GetByPhoneNumber(string? phoneNumber)
        {
            return await _context.Persons.Where(p => p.PhoneNumber == phoneNumber)
               .FirstOrDefaultAsync()!;
        }

        public async Task<List<Book>> GetPersonBooksAsync(int? id)
        {
            List<Book> books;
            books = await _context.Persons
                .Where(p => p.Id == id)
                .SelectMany(p => p.Books)
                .Include(b => b.Author)
                .ToListAsync();
            return books;
        }
        public async Task AddBookToPersonAsync(int personId, int bookId)
        {
            var person = await _context.Persons.Include(p => p.Books).FirstOrDefaultAsync(p => p.Id == personId);
            if (person == null)
            {
                throw new Exception("Person not found.");
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            person.Books.Add(book);
            //await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePersonBookAsync(int? personId, int? bookId)
        {
          
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Person_Book WHERE PersonsId = {0} AND BooksId = {1}", personId, bookId);
            Book? existed = await _context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Books)
                .SelectMany(p => p.Books)
                .Where(b => b.Id == bookId)
                .FirstOrDefaultAsync();

            return existed == null;
        }

        public async Task<Person> GetPersonWithBooksAsync(int? id)
        {
            var personWithBooks = await _context.Persons
                .Where(p => p.Id == id)
                .Include(p => p.Books)
                .FirstOrDefaultAsync();
            return personWithBooks;
        }
    }
}

