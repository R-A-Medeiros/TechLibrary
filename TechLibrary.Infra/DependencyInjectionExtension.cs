using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens;
using TechLibrary.Infra.DataAccess;
using TechLibrary.Infra.DataAccess.Repositories;
using TechLibrary.Infra.Security.Tokens.Access;

namespace TechLibrary.Infra;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);

        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>();

    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDoLoginRepository, DoLoginRepositorie>();
        services.AddScoped<IBookRepository, BookRepository>();
       
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var version = new Version(8, 0, 39);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<TechLibraryDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
