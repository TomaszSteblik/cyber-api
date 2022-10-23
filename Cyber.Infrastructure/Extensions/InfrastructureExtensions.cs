using System.Reflection;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using Cyber.Infrastructure.Factories;
using Cyber.Infrastructure.Middlewares;
using Cyber.Infrastructure.Repositories;
using Cyber.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IJwtService, JwtService>();
        serviceCollection.AddScoped<IMongoClient, MongoClient>(s =>
            new MongoClient(s.GetRequiredService<IConfiguration>()["MongoConnectionString"]));
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
        serviceCollection.AddTransient<ExceptionToHttpMiddleware>();
        serviceCollection.AddScoped<IPreviousPasswordsRepository, PreviousPasswordsRepository>();
        serviceCollection.AddScoped<IMailingService, MailingService>();
        serviceCollection.AddScoped<IMailMessageFactory, MailMessageFactory>();
        serviceCollection.AddScoped<IPasswordPoliciesRepository, PasswordPoliciesRepository>();
        serviceCollection.AddScoped<IUserPasswordExpirySettingRepository, UserPasswordExpirySettingRepository>();
        return serviceCollection;
    }
}