using Business.Data.Messages;

namespace Business.Data.Interfaces;

public interface IBookService
{
    public Task<AddBookResponse> AddBook(AddBookRequest request);
    public Task<GetCompletedBookResponse> GetCompletedBook();
    public Task<SearchBookResponse> SearchBook(SearchBookRequest request);
}