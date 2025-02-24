using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    public string Generate(User user);
}
