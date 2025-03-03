using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infra.DataAccess.Repositories;
internal class DoLoginRepositorie : IDoLoginRepository
{
    private readonly TechLibraryDbContext _context;

    public DoLoginRepositorie(TechLibraryDbContext context)
    {
        _context = context;
    }


    public async Task<User> GetUserAsync(User request)
    {
       return  await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(request.Email));
    }
}
