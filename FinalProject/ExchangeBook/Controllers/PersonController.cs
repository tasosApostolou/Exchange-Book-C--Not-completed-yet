using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.DTO.PersonDTO;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.Services;
using ExchangeBook.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExchangeBook.Controllers
{
    public class PersonController :BaseController
    {
        private readonly IMapper _mapper;

        public PersonController(IApplicationService applicationService,
        IMapper mapper) : base(applicationService)
        {
            _mapper = mapper;
        }

        [HttpPost("{personId}/books")]
        public async Task<IActionResult> AddBookToPerson(int personId,[FromBody] BookInsertDTO bookDto)
        {
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

                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            var book = await _applicationService.BookService.CreateBookAsync(bookDto);

            // Add the created book to the person's books (assigning personId and Bookid in Person_Book many-many table
            await _applicationService.PersonService.AddBookToPersonAsync(personId, book.Id);

            return Ok(_mapper.Map<BookReadOnlyDTO>(book));
            //return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadOnlyDTO>> GetBook(int id)
        {
            Book? book = await _applicationService.BookService.GetBookWithAuthorById(id);
            if (book is null)
            {
                throw new BookNotFoundException("Book NotFound");
            }

            BookReadOnlyDTO returnedBook = _mapper.Map<BookReadOnlyDTO>(book);
            //AuthorReadonlyDTO authorReadonly= _mapper.Map<AuthorReadonlyDTO>(book.Author);
           
            return Ok(returnedBook);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Book>>> GetPersonBooks(int? id)
        {
            List<Book> books = await _applicationService.PersonService.GetPersonBooksAsync(id);
            List<BookReadOnlyDTO> booksReadOnly;
            if(books is null)
            {
                throw new BookNotFoundException("not found books");
            }         
                return Ok(_mapper.Map<List<BookReadOnlyDTO>>(books));        
        }

        [HttpDelete("personal/{personId}/book/{bookId}")]
        public async Task<ActionResult<BookReadOnlyDTO>> DeletesPersonBook(int personId, int bookId)
        {
          
             BookReadOnlyDTO deletedDTO = await _applicationService.PersonService.DeletePersonBookAsync(personId, bookId);
            if (deletedDTO == null)
            {
                throw new ServerGenericException("book not found or not deleted or server error");
            }
                //return Ok("Deleted");
                return Ok(deletedDTO);
        }

        [HttpPut("{personId}")]
        //[Authorize(Roles = "Personal")]
        public async Task<ActionResult<PersonReadOnlyDTO>> UpdateUserAccount(int personId, PersonDTO? personDTO)
        {
           

            var person = await _applicationService.PersonService.UpdatePersonAsync(personId, personDTO!);
            var returnedPersonDTO = _mapper.Map<PersonReadOnlyDTO>(person);
            return Ok(returnedPersonDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeletePerson(int id)
        {
            
            await _applicationService.PersonService.DeletePersonAsync(id);

            return "Deleted";
        }
    }
}
