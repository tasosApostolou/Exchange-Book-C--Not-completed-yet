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
        public async Task AddBookToPersonAsync(Person? person, Book? book)
        {

            person.Books.Add(book);
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

        public async Task<Person?> UpdatePersonAsync(int personId, Person person)
        {
            var existingPerson = _context.Persons.FirstOrDefault(p => p.Id == personId);
            if (existingPerson.Id != personId && personId != person.Id)
            {
                return null;

            }
            if (existingPerson != null)
            {
                //Μου εβγαζε ενα error οτι δεν μπορει να γινει attach με new instance, να θυμηθω να το αλλαξω!
                _context.Entry(existingPerson).State = EntityState.Detached;
            }


            // Attach the new entity instance and set its state to Modified
            _context.Persons.Attach(person);
            _context.Entry(person).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return person;
        }
    }
}

