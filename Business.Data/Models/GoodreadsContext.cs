using Microsoft.EntityFrameworkCore;

namespace Business.Data.Models;

public class GoodreadsContext: DbContext
{
    public GoodreadsContext(DbContextOptions<GoodreadsContext> options)
        : base(options)
    {
    }
    public GoodreadsContext() { }

    public virtual DbSet<Book> Books { get; set; }
}
