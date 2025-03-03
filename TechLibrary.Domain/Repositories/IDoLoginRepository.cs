using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories;

public interface IDoLoginRepository
{
    Task<User> GetUserAsync(User user);
}

