using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Business.Data.Messages;
using Business.Data.Models;
using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Goodreads.Test;

public class BookServiceTest
{
    [Fact]
    public async void GetCompletedBookTest()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.GetCompletedBook();

        var count = results.Books.Count();

        // assert
        Assert.Equal(5, count);
    }
    [Fact]
    public async void SearchBookTest()
    {
        // arrange
        var context = CreateDbContextMock();

        var service = new BookService(context.Object);

        // act
        var results = await service.SearchBook(new SearchBookRequest { Name = "Start" });

        var count = results.Books.Count();

        // assert
        Assert.Equal(1, count);
    }

    [Fact]
    public async void AddBookTest()
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

        var status = results.Status;

        // assert
        Assert.Equal(expected: true, status);
    }

    private Mock<GoodreadsContext> CreateDbContextMock()
    {
        var books = GetFakeData().AsQueryable();

        var dbSet = new Mock<DbSet<Book>>();
        //dbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
        //dbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
        //dbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
        //dbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

        dbSet.As<IDbAsyncEnumerable<Book>>()
            .Setup(m => m.GetAsyncEnumerator())
            .Returns(new TestDbAsyncEnumerator<Book>(books.GetEnumerator()));

        dbSet.As<IQueryable<Book>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Book>(books.Provider));

        dbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
        dbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
        dbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

        var context = new Mock<GoodreadsContext>();
        context.Setup(c => c.Books).Returns(dbSet.Object);
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
    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }
}