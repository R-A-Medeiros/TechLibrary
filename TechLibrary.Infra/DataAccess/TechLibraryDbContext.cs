
using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infra.DataAccess;
internal class TechLibraryDbContext : DbContext
{
    public TechLibraryDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; } 
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
 
}
