using System.Reflection;
using Cyber.Application.Services;
using Cyber.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cyber.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
        serviceCollection.AddSingleton(Random.Shared);
        serviceCollection.AddScoped<IOneTimePasswordCalculatorService, OneTimePasswordCalculatorService>();
        return serviceCollection;
    }
}