using AutoMapper;
using Entities.DataTransferObject;
using Entities.DataTransferObject.CategoryDto;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate,Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();

            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDtoForInsertion, Category>();

            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
