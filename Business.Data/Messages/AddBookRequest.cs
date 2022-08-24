using Business.Data.Models;

namespace Business.Data.Messages;

public class AddBookRequest
{
    public Book Book { get; set; }
}