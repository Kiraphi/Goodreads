using Business.Data.Interfaces;
using Business.Data.Models;
using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Config swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Goodreads Api", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();


// db
builder.Services.AddDbContext<GoodreadsContext>
    (o => o.UseInMemoryDatabase("Goodreads"));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Use the default property (Pascal) casing
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    //Keep local time
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
});

// Add DI
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

AddBookData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void AddBookData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<GoodreadsContext>();

    db.Books.AddRange(new List<Book>
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

    });

    db.SaveChanges();
}