using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.AuthorDTOs;
using ExchangeBook.DTO.BookDTOs;
using ExchangeBook.DTO.PersonDTO;
using ExchangeBook.DTO.UserDTOs;

namespace ExchangeBook.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
        //    CreateMap<User, UserPatchDTO>().ReverseMap();
            CreateMap<User, UserSignUpDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserReadOnlyDTO>().ReverseMap();
            CreateMap<User, UserPersonReadOnlyDTO>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Person!.Id}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Person!.PhoneNumber}"))
                .ForMember(dest => dest.personId, opt => opt.MapFrom(src => src.Person != null ? src.Person.Id : (int?)null))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => $"{src.Person!.Firstname}"))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => $"{src.Person!.Lastname}")).ReverseMap();
            CreateMap<User, UserStoreReadOnlyDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Store!.Name}"))
                .ForMember(dest => dest.storeId, opt => opt.MapFrom(src => src.Store != null ? src.Store.Id : (int?)null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Store!.Address}")).ReverseMap();

            //CreateMap<AuthorReadOnlyDTo, AuthorInsertDTO>().ReverseMap();
            CreateMap<AuthorInsertDTO, Author>().ReverseMap();
            CreateMap<BookInsertDTO, Book>()

                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();
            //CreateMap<Book, BookReadOnlyDTO>()
            //    .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            //    .ReverseMap();

            CreateMap<Author, AuthorReadonlyDTO>().ReverseMap();

            CreateMap<Book, BookReadOnlyDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            CreateMap<StoreBookInsertDTO, StoreBook>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            //CreateMap<StoreBook, StoreBookReadOnlyDTO>().ReverseMap();
            CreateMap<StoreBook, StoreBookReadOnlyDTO>()
              .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
              .ReverseMap();

            CreateMap<Book, BookReadDTOIncludePersons>()
                .ForMember(dest => dest.Persons, opt => opt.MapFrom(src => src.Persons))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author)).ReverseMap();
            //CreateMap<Person, PersonReadOnlyDTO>() 
            //    .ForMember(dest => dest.)



            CreateMap<Person, PersonReadOnlyDTO>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
      .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
      .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
      .ReverseMap();

        }
    }
}
