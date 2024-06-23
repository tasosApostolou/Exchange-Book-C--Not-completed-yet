using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExchangeBook.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookExchangeDbContext context) : base(context)
        {

        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _context.Authors.Where(a => a.Name == name)
                            .FirstOrDefaultAsync()!;
        }

        public Task<List<Author>> GetAuthorsByBookTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

    }
}
