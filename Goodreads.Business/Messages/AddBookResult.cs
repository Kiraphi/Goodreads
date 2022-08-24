using System.Net;

namespace Goodreads.Business.Messages;

public class AddBookResult
{
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}