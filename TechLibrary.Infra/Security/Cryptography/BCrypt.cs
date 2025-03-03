
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace TechLibrary.Infra.Security.Cryptography;
internal class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, User user) => BC.Verify(password, user.Password);
}
