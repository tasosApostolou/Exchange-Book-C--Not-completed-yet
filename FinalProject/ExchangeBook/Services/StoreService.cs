using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Repositories;

namespace ExchangeBook.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public StoreService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddBookToStoreAsync(int storeId, double price, Book? book)
        {
            Store store = await _unitOfWork.StoreRepository.GetAsync(storeId);
            StoreBook? storeBook = new()
            {
                Store = store,
                Book = book,
                Price = price
            };
            //
            await _unitOfWork.StoreBookRepository.AddAsync(storeBook);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<StoreBook>> GetStoreBooksByStoreIdAsync(int storeId)
        {
            Store store = await _unitOfWork.StoreRepository.GetStoreIncludeBooksAsync(storeId);
            return await _unitOfWork.StoreBookRepository.GetStoreBooksByStoreIdAsync(storeId);
        }
    }
}
