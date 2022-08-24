using System.Net;

namespace Business.Data.Messages;

public class AddBookResponse
{
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}