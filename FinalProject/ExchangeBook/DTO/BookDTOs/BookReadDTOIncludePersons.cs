using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using ExchangeBook.DTO.PersonDTO;

namespace ExchangeBook.DTO.BookDTOs
{
    public class BookReadDTOIncludePersons
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        //public string Description { get; set; }
        public AuthorReadonlyDTO? Author { get; set; }
        public ICollection<PersonDTO.PersonReadOnlyDTO?> Persons { get; set; }
    }
}
