using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infra.DataAccess.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly TechLibraryDbContext _context;

    public UserRepository(TechLibraryDbContext context)
    {
        _context = context;
    }
    public async Task Add(User user)
    {
       await _context.Users.AddAsync(user);
    }
}
