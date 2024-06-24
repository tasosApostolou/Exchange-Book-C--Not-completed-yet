using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Services;
using ExchangeBook.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeBook.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IMapper _mapper;

        public StoreController(IApplicationService applicationService,
        IMapper mapper) : base(applicationService)
        {
            _mapper = mapper;
        }
        [HttpPost("{storeId}/books")]
        public async Task<IActionResult> AddBookToStore(int storeId, [FromBody] StoreBookInsertDTO storeBookInsertDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, build a custom response
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                // instead of return BadRequest(new { Errors = errors });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            BookInsertDTO? bookDTO = storeBookInsertDTO.Book;
            var book = await _applicationService.BookService.CreateBookAsync(bookDTO);

            // Add the created book to the store's books (assigning storeId and Bookid in StoreBooks many-many table
            await _applicationService.StoreService.AddBookToStoreAsync(storeId, storeBookInsertDTO.Price, book);
            return Ok(_mapper.Map<BookReadOnlyDTO>(book));

        }
    }
}
