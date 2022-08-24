using Goodreads.Business.Messages;

namespace Goodreads.Business.Interfaces;

public interface IBookBusiness
{
    public Task<AddBookResult> AddBook(AddBookParam request);
    public Task<GetCompletedBookResult> GetCompletedBook();
    public Task<SearchBookResult> SearchBook(SearchBookParam request);
}