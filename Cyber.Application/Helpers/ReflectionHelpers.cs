using Cyber.Domain.Policies.PasswordPolicy;

namespace Cyber.Application.Helpers;

public static class ReflectionHelpers
{
    public static IEnumerable<string> GetPasswordPoliciesNamesFromAssembly()
    {
        var policiesTypes = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IPasswordPolicy).IsAssignableFrom(type));

        return policiesTypes.Select(type => type.Name).Where(x => x != nameof(IPasswordPolicy));
    }
}