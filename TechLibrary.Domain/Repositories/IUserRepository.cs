using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsEmailAsync(string email);
}
