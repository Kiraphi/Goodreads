using System.Net;
using Business.Data.Messages;
using Business.Data.Models;
using Business.Data.Repositories;
using MockQueryable.Moq;
using Moq;

namespace Goodreads.Test;

public class BookServiceTest
{
    [Fact]
    public async void GetCompletedBook_CallService_ReturnOkAndData()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.GetCompletedBook();

        var count = results.Books.Count();
        var statusCode = results.StatusCode;

        // assert
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(5, count);
    }
    [Fact]
    public async void SearchBook_WithParamName_ReturnOkAndData()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.SearchBook(new SearchBookRequest { Name = "Start" });

        var count = results.Books.Count();
        var statusCode = results.StatusCode;

        // assert
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(1, count);
    }

    [Fact]
    public async void AddBook_WithData_ReturnOk()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.AddBook(new AddBookRequest
        {
            Book = new Book
            {
                BookName = "AVC",
                IsCompleted = false,
                BookCode = "B0004",
            }
        });

        var statusCode = results.StatusCode;

        // assert
        Assert.Equal(HttpStatusCode.OK, statusCode);
    }

    [Fact]
    public async void AddBook_WithNoData_ReturnBadRequest()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.AddBook(new AddBookRequest
        {});

        var statusCode = results.StatusCode;

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, statusCode);
    }

    private Mock<GoodreadsContext> CreateDbContextMock()
    {
        var books = GetFakeData().AsQueryable();

        var mock = books.BuildMockDbSet();

        var context = new Mock<GoodreadsContext>();
        context.Setup(c => c.Books).Returns(mock.Object);
        return context;

    }


    private IEnumerable<Book> GetFakeData()
    {
        var books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B001",
                BookName = "Start free writing to find keywords",
                IsCompleted = false
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B002",
                BookName = "Experiment with word patterns",
                IsCompleted = true
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B003",
                BookName = "Draw inspiration from your characters",
                IsCompleted = true
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B004",
                BookName = "Keep your setting in mind",
                IsCompleted = false
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B005",
                BookName = "Look for book title ideas in famous phrases",
                IsCompleted = true
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B006",
                BookName = "Analyze the book titles of other books",
                IsCompleted = false
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B007",
                BookName = "Don’t forget the subtitle",
                IsCompleted = true
            },
            new Book
            {
                Id = Guid.NewGuid(),
                BookCode = "B008",
                BookName = "Generate a book name through a book title generator",
                IsCompleted = true
            },
        };
        return books;
    }
}