using AutoMapper;
using Business.Data.Messages;
using Business.Data.Models;
using Goodreads.Business.Dtos;
using Goodreads.Business.Messages;

namespace Goodreads.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<BookDto, Book>().ReverseMap();

        CreateMap<AddBookParam, AddBookRequest>();
        CreateMap<AddBookResponse, AddBookResult>();

        CreateMap<GetCompletedBookResponse, GetCompletedBookResult>();
        CreateMap<SearchBookParam, SearchBookRequest>();
        CreateMap<SearchBookResponse, SearchBookResult>();
    }
}