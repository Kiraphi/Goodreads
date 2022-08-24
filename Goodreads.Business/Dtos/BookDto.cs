namespace Goodreads.Business.Dtos;

public class BookDto
{
    public Guid? Id { get; set; }
    public string BookName { get; set; }
    public string BookCode { get; set; }
    public bool IsCompleted { get; set; }
}