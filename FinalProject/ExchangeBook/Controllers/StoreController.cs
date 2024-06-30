using AutoMapper;
using ExchangeBoodk.Services;
using ExchangeBook.Data;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.Services;
using ExchangeBook.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

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
        [HttpPost("{storeId}")]
        public async Task<ActionResult<BookReadOnlyDTO>> AddBookToStore(int storeId, [FromBody] StoreBookInsertDTO storeBookInsertDTO)
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

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<List<StoreBookReadOnlyDTO>>> GetStoreBooksByStoreId(int storeId)
        {
            var storeBooks = await _applicationService.StoreBookService.GetStoreBooksByStoreIdAsync(storeId);
            //var storeBooks = await _applicationService.StoreService.GetStoreBooksByStoreIdAsync(storeId);

            if (storeBooks == null || storeBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<StoreBookReadOnlyDTO>>(storeBooks));

            //return Ok(storeBooks);
        }

        [HttpGet("title/{initials}")]
        public async Task<ActionResult<List<StoreBookReadOnlyDTO>>> GetStoreBooksByBookTitle(string initials)
        {
            var storeBooks = await _applicationService.StoreBookService.GetStoreBooksByBookTitleAsync(initials);
            if (storeBooks == null || storeBooks.Count == 0)
            {
                return NotFound();
            }
            return _mapper.Map<List<StoreBookReadOnlyDTO>>(storeBooks);
        }

        [HttpDelete("{storeId}/books/{bookId}")]
        public async Task<ActionResult<StoreBookReadOnlyDTO>> RemoveBookFromStore(int storeId, int bookId)
        {
            StoreBook? storeBook = await _applicationService.StoreBookService.RemoveBookFromStoreAsync(storeId, bookId);
            return _mapper.Map<StoreBookReadOnlyDTO>(storeBook);
        }
    }
}
