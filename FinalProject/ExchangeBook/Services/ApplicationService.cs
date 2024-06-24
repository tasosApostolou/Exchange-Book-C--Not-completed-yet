using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.Repositories;

namespace ExchangeBook.Services
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService>? _logger;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public UserService UserService => new(_unitOfWork, _logger, _mapper);
        public PersonService PersonService => new(_unitOfWork, _logger, _mapper);
        public StoreService StoreService => new(_unitOfWork, _logger, _mapper);
        public BookService BookService => new(_unitOfWork, _logger, _mapper);
        public StoreBookService StoreBookService => new(_unitOfWork, _logger, _mapper);
        





        //public UserService UserService => throw new NotImplementedException();
    }
}
