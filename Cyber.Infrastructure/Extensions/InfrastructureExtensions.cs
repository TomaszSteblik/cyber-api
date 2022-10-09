using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using Cyber.Infrastructure.Middlewares;
using Cyber.Infrastructure.Repositories;
using Cyber.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cyber.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IJwtService, JwtService>();
        serviceCollection.AddSingleton<IUsersRepository, UsersRepository>();
        serviceCollection.AddTransient<ExceptionToHttpMiddleware>();
        serviceCollection.AddSingleton<IPreviousPasswordsRepository, PreviousPasswordsRepository>();
        return serviceCollection;
    }
}