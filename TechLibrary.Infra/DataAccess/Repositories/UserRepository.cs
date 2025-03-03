using Microsoft.EntityFrameworkCore;
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
    public async Task AddAsync(User user)
    {
       await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistsEmailAsync(string email)
    {
       var result = await _context.Users.
                        AnyAsync(user => user.Email.Equals(email));

        return result;
    }
}
