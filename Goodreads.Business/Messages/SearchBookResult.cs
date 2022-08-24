using System.Net;
using Goodreads.Business.Dtos;

namespace Goodreads.Business.Messages;

public class SearchBookResult
{
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<BookDto> Books { get; set; }
}