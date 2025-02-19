
using Microsoft.EntityFrameworkCore;

namespace TechLibrary.Infra.DataAccess;
internal class TechLibraryDbContext : DbContext
{
    public TechLibraryDbContext(DbContextOptions options) : base(options) { }

    //public DbSet<Users> Users { get; set; } // Fazer conexão com o banco mysql na proxima implementação
 
}
