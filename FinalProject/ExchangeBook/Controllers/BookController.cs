using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.DTO.PersonDTO;
using ExchangeBook.Services;
using ExchangeBook.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ExchangeBook.Controllers
{
    public class BookController : BaseController
    {
        private readonly IMapper _mapper;

        public BookController(IApplicationService applicationService,
        IMapper mapper) : base(applicationService)
        {
            _mapper = mapper;
        }

        [HttpGet("{title}/books")]
        public async Task<ActionResult<List<BookReadDTOIncludePersons>>> GetBooksByTitle(string? title)
        {
            List<Book>? books = await _applicationService.BookService.GetBooksByTitleAsync(title);
            if (books == null) { throw new BookNotFoundException("Not found Books"); }
            return Ok(_mapper.Map<List<BookReadDTOIncludePersons>>(books));
        }
    }
}
