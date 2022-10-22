using System.Reflection;
using Cyber.Application.DTOs.Read;
using Cyber.Application.Exceptions;
using Cyber.Application.Helpers;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Queries.GetUserPolicies;

internal class GetUserPasswordPoliciesHandler : IRequestHandler<GetUserPasswordPoliciesQuery, IEnumerable<GetPasswordPolicyDto>>
{
    private readonly IPasswordPoliciesRepository _passwordPoliciesRepository;
    private readonly IUsersRepository _usersRepository;

    public GetUserPasswordPoliciesHandler(IPasswordPoliciesRepository passwordPoliciesRepository,
        IUsersRepository usersRepository)
    {
        _passwordPoliciesRepository = passwordPoliciesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<GetPasswordPolicyDto>> Handle(GetUserPasswordPoliciesQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        var policies = new List<GetPasswordPolicyDto>();
        foreach (var key in ReflectionHelpers.GetPasswordPoliciesNamesFromAssembly())
        {
            policies.Add(new GetPasswordPolicyDto()
            {
                Key = key,
                Enabled = await _passwordPoliciesRepository.GetEnabledStatusByPolicyKey(key, request.UserId)
            });
        }

        return policies;
    }
}