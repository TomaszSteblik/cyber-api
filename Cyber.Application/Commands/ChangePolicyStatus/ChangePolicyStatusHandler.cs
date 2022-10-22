using Cyber.Application.Exceptions;
using Cyber.Application.Helpers;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.ChangePolicyStatus;

internal class ChangePolicyStatusHandler : IRequestHandler<ChangePolicyStatusCommand>
{
    private readonly IPasswordPoliciesRepository _passwordPoliciesRepository;
    private readonly IUsersRepository _usersRepository;

    public ChangePolicyStatusHandler(IPasswordPoliciesRepository passwordPoliciesRepository,
        IUsersRepository usersRepository)
    {
        _passwordPoliciesRepository = passwordPoliciesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<Unit> Handle(ChangePolicyStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        if (ReflectionHelpers.GetPasswordPoliciesNamesFromAssembly().Contains(request.Key) is false)
            throw new PasswordPolicyNotFoundException(request.Key);

        await _passwordPoliciesRepository.SaveEnabledStatus(request.Key, request.Status, request.UserId);
        return Unit.Value;
    }
}