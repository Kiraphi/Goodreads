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

    /// <summary>
    /// Add a book to the list of books they have read
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AddBookResponse> AddBook(AddBookRequest request)
    {
        if (request.Book == null)
        {
            return new AddBookResponse
            {
                Status = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        request.Book.Id = Guid.NewGuid();
        await _context.Books.AddAsync(request.Book);
        await _context.SaveChangesAsync();
        return new AddBookResponse
        {
            Status = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    /// <summary>
    /// View a list of books they have completed reading
    /// </summary>
    /// <returns>List of Book</returns>
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

    /// <summary>
    /// Search for a book by name
    /// </summary>
    /// <param name="request">Name</param>
    /// <returns>List of Book</returns>
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