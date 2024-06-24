using AutoMapper;
using ExchangeBoodk.Services;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Repositories;

namespace ExchangeBook.Services
{
    public class StoreBookService: IStoreBookService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public StoreBookService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<StoreBook>> GetStoreBooksByStoreIdAsync(int storeId)
        {
            return await _unitOfWork.StoreBookRepository.GetStoreBooksByStoreIdAsync(storeId);
        }

    }
}
