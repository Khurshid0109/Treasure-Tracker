using TreasureTracker.Data.IRepositories;
using TreasureTracker.Data.Repositories;
using TreasureTracker.Domain.IRepositories;
using TreasureTracker.Domain.Repositories;
using TreasureTracker.Service.Interfaces.Auth;
using TreasureTracker.Service.Interfaces.UserCodes;
using TreasureTracker.Service.Interfaces.Users;
using TreasureTracker.Service.Services.Auth;
using TreasureTracker.Service.Services.UserCodes;
using TreasureTracker.Service.Services.Users;

namespace TreasureTracker.UI.Extentions;
public static class ServiceExtention
{
    public static void AddServices(this IServiceCollection services)
    {
        // Auth
        services.AddScoped<IExistEmail, ExistEmail>();
        services.AddScoped<IUserAuthentication,UserAuthentication>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Generic Repository
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

        // User
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        // UserCode
        services.AddScoped<IUserCodeService, UserCodeService>();
        services.AddScoped<IUserCodeRepository, UserCodeRepository>();
    }
}
