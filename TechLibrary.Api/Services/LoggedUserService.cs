using System.IdentityModel.Tokens.Jwt;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Api.Services;

public class LoggedUserService
{
    private readonly HttpContext _httpContext;

    public LoggedUserService(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public User User()
    {
      var authentication = _httpContext.Request.Headers.Authorization.ToString();
      var token = authentication["Bearer".Length..].Trim();
      var tokenHandler = new JwtSecurityTokenHandler();
      var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

      var identifier = jwtSecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;

      var userId = Guid.Parse(identifier);

      var dbContext = new TechLibraryDbContext();
        
      // Ainda não está finalizado, mover essa logica para respectiva camanda e fazer a injeção de dependencia correta

      return dbContext.Users.First(User => User.Id == userId);  
    }
}
