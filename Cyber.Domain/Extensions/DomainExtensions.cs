using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cyber.Domain.Extensions;

public static class DomainExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPasswordPolicy, NumbersPolicy>();
        serviceCollection.AddScoped<IPasswordPolicy, UppercaseLettersPolicy>();
        serviceCollection.AddScoped<IPasswordPolicy, LowercaseLettersPolicy>();
        serviceCollection.AddScoped<IUserLoginAttemptsBlockService, UserLoginAttemptsBlockService>();
        return serviceCollection;
    }
}