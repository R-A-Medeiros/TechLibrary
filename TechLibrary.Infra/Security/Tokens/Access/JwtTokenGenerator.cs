using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Security.Tokens;

namespace TechLibrary.Infra.Security.Tokens.Access;
internal class JwtTokenGenerator : IAccessTokenGenerator
{
    public string Generate(User user)
    {
        var claims = new List<Claim>();
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(60),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var  securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private static SymmetricSecurityKey SecurityKey()
    {
        //Chave para teste
        var key = "yQt6PwEofGdvVvQrs429q08HA1AJIJ17RXF6KtI2f2sMg";

        var symmetricKey = Encoding.UTF8.GetBytes(key);

        return new SymmetricSecurityKey(symmetricKey);
    }
}
