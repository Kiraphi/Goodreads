using Goodreads.Business.Dtos;

namespace Goodreads.Business.Messages;

public class AddBookParam
{
    public BookDto Book { get; set; }
}