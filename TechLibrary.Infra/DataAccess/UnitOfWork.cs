using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infra.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly TechLibraryDbContext _dbContext;

    public UnitOfWork(TechLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
