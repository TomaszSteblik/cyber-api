using System.Reflection;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using Cyber.Infrastructure.Factories;
using Cyber.Infrastructure.Middlewares;
using Cyber.Infrastructure.Options;
using Cyber.Infrastructure.Repositories;
using Cyber.Infrastructure.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration builderConfiguration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddScoped<IMongoClient, MongoClient>(s =>
            new MongoClient(s.GetRequiredService<IConfiguration>().GetConnectionString("Mongo")));

        serviceCollection.AddAzureClients(x =>
        {
            x.AddServiceBusClient(builderConfiguration.GetConnectionString("ServiceBus"));
        });

        serviceCollection.AddTransient<ExceptionToHttpMiddleware>();

        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IMailingService, MailingService>();

        serviceCollection.AddScoped<IMailMessageFactory, MailMessageFactory>();

        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
        serviceCollection.AddScoped<IPreviousPasswordsRepository, PreviousPasswordsRepository>();
        serviceCollection.AddScoped<IPasswordPoliciesRepository, PasswordPoliciesRepository>();
        serviceCollection.AddScoped<IUserPasswordExpirySettingRepository, UserPasswordExpirySettingRepository>();
        serviceCollection.AddScoped<ILoginAttemptsRepository, LoginAttemptsRepository>();
        serviceCollection.AddScoped<IOneTimePasswordRepository, OneTimePasswordRepository>();
        serviceCollection.AddScoped<IConfigRepository, ConfigRepository>();
        serviceCollection.AddScoped<ICaptchaService, CaptchaService>();

        serviceCollection.AddScoped<IMessageBroker, AzureServiceBusBroker>();
        serviceCollection.AddHttpClient();

        serviceCollection.AddHostedService<OldLoginAttemptsRemovalHostedService>();

        serviceCollection.AddOptions<CaptchaOptions>(CaptchaOptions.CaptchaSectionName);

        return serviceCollection;
    }
}