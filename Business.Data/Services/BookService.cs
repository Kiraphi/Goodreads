using System.Net;
using Business.Data.Interfaces;
using Business.Data.Messages;
using Business.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Data.Repositories;

public class BookService : IBookService
{
    private readonly GoodreadsContext _context;

    public BookService(GoodreadsContext context)
    {
        _context = context;
    }

    public async Task<AddBookResponse> AddBook(AddBookRequest request)
    {
        request.Book.Id = Guid.NewGuid();
        await _context.Books.AddAsync(request.Book);
        await _context.SaveChangesAsync();
        return new AddBookResponse
        {
            Status = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GetCompletedBookResponse> GetCompletedBook()
    {
        var books = await _context.Books.Where(x => x.IsCompleted).ToListAsync();
        return new GetCompletedBookResponse
        {
            Books = books,
            StatusCode = HttpStatusCode.OK,
            Status = true
        };
    }

    public async Task<SearchBookResponse> SearchBook(SearchBookRequest request)
    {
        var books = await _context.Books.Where(x => x.BookName.Contains(request.Name) || String.IsNullOrEmpty(request.Name)).ToListAsync();
        return new SearchBookResponse
        {
            Books = books,
            StatusCode = HttpStatusCode.OK,
            Status = true
        };
    }
}