using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool Verify(string password, User user);
}
