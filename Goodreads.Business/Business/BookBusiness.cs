using System.Net;
using AutoMapper;
using Business.Data.Interfaces;
using Business.Data.Messages;
using Goodreads.Business.Interfaces;
using Goodreads.Business.Messages;
using Microsoft.Extensions.Logging;

namespace Goodreads.Business.Business;

public class BookBusiness : IBookBusiness
{
    private readonly IBookRepo _bookRepo;
    private readonly ILogger<BookBusiness> _logger;
    private readonly IMapper _mapper;
    public BookBusiness(IBookRepo bookRepo, ILogger<BookBusiness> logger, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AddBookResult> AddBook(AddBookParam param)
    {
        try
        {
            var request = _mapper.Map<AddBookRequest>(param);
            var response = await _bookRepo.AddBook(request);
            var result = _mapper.Map<AddBookResult>(response);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new AddBookResult
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Status = false
            };
        }
    }

    public async Task<GetCompletedBookResult> GetCompletedBook()
    {
        try
        {
            var response = await _bookRepo.GetCompletedBook();
            var result = _mapper.Map<GetCompletedBookResult>(response);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new GetCompletedBookResult
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Status = false
            };
        }
    }

    public async Task<SearchBookResult> SearchBook(SearchBookParam param)
    {
        try
        {
            var request = _mapper.Map<SearchBookRequest>(param);
            var response = await _bookRepo.SearchBook(request);
            var result = _mapper.Map<SearchBookResult>(response);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new SearchBookResult
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Status = false
            };
        }
    }
}