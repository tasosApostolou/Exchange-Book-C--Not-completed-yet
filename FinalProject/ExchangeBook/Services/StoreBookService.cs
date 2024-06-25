using AutoMapper;
using ExchangeBoodk.Services;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Repositories;
using ExchangeBook.Services.Exceptions;

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

        public async Task<List<StoreBook>> GetStoreBooksByBookTitleAsync(string title)
        {
            List<StoreBook?> storeBooks;
            try
            {
                storeBooks = await _unitOfWork.StoreBookRepository.GetByBookTitleAsync(title);
                if(storeBooks == null)
                {
                    throw new BookNotFoundException("book with title: "+ title +" not found in stores" );
                }
                return (storeBooks);
            }
            catch(BookNotFoundException e) 
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }

        public async Task RemoveBookFromStoreAsync(int storeId, int bookId)
        {
            bool deleted;
            try
            {
                deleted = await _unitOfWork.StoreBookRepository.RemoveBookFromStoreAsync(storeId,bookId);
                if (!deleted)
                {
                    throw new BookNotFoundException("Book Not Found");
                }
                await _unitOfWork.SaveAsync();
              
            }
            catch (BookNotFoundException e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }

            //bool deleted;
            //try
            //{
            //    deleted = await _unitOfWork!.UserRepository.DeleteAsync(id);
            //    if (!deleted)
            //    {
            //        throw new UserNotFoundException("UserNotFound");
            //    }
            //}
            //catch (Exception e)
            //{
            //    _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            //    throw;
            //}
        }
    }
}
