using AutoMapper;
using ExchangeBook.Data;

namespace ExchangeBook.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly BookExchangeDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(BookExchangeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookRepository BookRepository => new(_context);
        public PersonRepository PersonRepository => new(_context);
        public StoreRepository StoreRepository => new (_context);
        public StoreBookRepository StoreBookRepository => new(_context);
        public AuthorRepository AuthorRepository => new(_context);
        public UserRepositorty UserRepositorty => new(_context, _mapper);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
