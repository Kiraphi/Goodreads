using Business.Data.Interfaces;
using Business.Data.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("GetCompletedBook")]
    public async Task<GetCompletedBookResponse> GetCompletedBook()
    {
        var result = await _bookService.GetCompletedBook();
        return result;
    }

    [HttpPost("SearchBook")]
    public async Task<SearchBookResponse> SearchBook(SearchBookRequest request)
    {
        var result = await _bookService.SearchBook(request);
        return result;
    }
    [HttpPost("AddBook")]
    public async Task<AddBookResponse> AddBook(AddBookRequest request)
    {
        var result = await _bookService.AddBook(request);
        return result;
    }
}