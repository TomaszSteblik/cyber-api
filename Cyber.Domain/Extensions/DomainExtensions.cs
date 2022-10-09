using Cyber.Domain.Policies.PasswordPolicy;
using Microsoft.Extensions.DependencyInjection;

namespace Cyber.Domain.Extensions;

public static class DomainExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPasswordPolicy, NumbersPolicy>();
        serviceCollection.AddScoped<IPasswordPolicy, UppercaseLettersPolicy>();
        serviceCollection.AddScoped<IPasswordPolicy, LowercaseLettersPolicy>();
        return serviceCollection;
    }
}