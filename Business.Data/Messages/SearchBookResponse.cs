using System.Net;
using Business.Data.Models;

namespace Business.Data.Messages;

public class SearchBookResponse
{
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<Book> Books { get; set; }
}