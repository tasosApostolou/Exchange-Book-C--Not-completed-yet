using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookExchangeDbContext context) : base(context)
        {
        }

        public async Task<List<Person>> GetBookPersonsAsync(int id)
        {
            List<Person> persons;
            persons = await _context.Books
                .Where(b => b.Id == id)
                .SelectMany(b => b.Persons)
                .ToListAsync();
            return persons;
        }

        public async Task<List<Book>> GetBooksByTitle(string? title)
        {
            List<Book> books;
            books = await _context.Books
               .Where(b => b.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase))
               .ToListAsync();
            return books;
        }
        public async Task<List<Book>> GetBooksByTitleIncludeAuthor(string? title)
        {
            List<Book> books;
            books = await _context.Books
               .Where(b => b.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase))
               .Include(b => b.Author)
               .ToListAsync();
            return books;
        }


        public async Task<Book> GetBookWithAuthorByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return null;
            }
            return book;
        }
    }
}
