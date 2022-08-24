using System.Net;
using Business.Data.Interfaces;
using Business.Data.Messages;
using Business.Data.Models;
using Goodreads.Api.Controllers;
using Moq;

namespace Goodreads.Test;

public class BookControllerTest
{

    [Fact]
    public async void GetCompletedBookTest()
    {
        // arrange
        var service = new Mock<IBookService>();

        var books = GetCompletedBookFakeData();

        service.Setup(x => x.GetCompletedBook()).Returns(Task.FromResult(books));

        var controller = new BookController(service.Object);

        // act
        var results = controller.GetCompletedBook();

        var count = results.Result.Books.Count();

        // assert
        Assert.Equal(26, count);
    }

    [Fact]
    public async void SearchBookTest()
    {
        // arrange
        var service = new Mock<IBookService>();

        var books = SearchBookFakeData();

        var request = new SearchBookRequest
        {
            Name = String.Empty
        };

        service.Setup(x => x.SearchBook(request)).Returns(Task.FromResult(books));

        var controller = new BookController(service.Object);

        // act
        var results = controller.SearchBook(request);

        var count = results.Result.Books.Count();

        // assert
        Assert.Equal(26, count);
    }

    [Fact]
    public async void AddBookTest()
    {
        // arrange
        var service = new Mock<IBookService>();

        var data = AddBookFakeData();

        var request = new AddBookRequest
        {
            Book = new Book
            {
                BookName = "AVC",
                IsCompleted = false,
                BookCode = "B0004",
            }
        };
        service.Setup(x => x.AddBook(request)).Returns(Task.FromResult(data));


        var controller = new BookController(service.Object);

        // act
        var results = controller.AddBook(request);

        var status = results.Result.Status;

        // assert
        Assert.Equal(expected: true, status);
    }

    private GetCompletedBookResponse GetCompletedBookFakeData()
    {
        var books = GenFu.GenFu.ListOf<Book>(26);
        books.ForEach(x => x.Id = Guid.NewGuid());
        return new GetCompletedBookResponse
        {
            Status = true,
            Books = books,
            StatusCode = HttpStatusCode.OK
        };
    }
    private SearchBookResponse SearchBookFakeData()
    {
        var books = GenFu.GenFu.ListOf<Book>(26);
        books.ForEach(x => x.Id = Guid.NewGuid());
        return new SearchBookResponse
        {
            Status = true,
            Books = books,
            StatusCode = HttpStatusCode.OK
        };
    }

    private AddBookResponse AddBookFakeData()
    {
        return new AddBookResponse
        {
            Status = true,
            StatusCode = HttpStatusCode.OK
        };
    }
}