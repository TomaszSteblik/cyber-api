using Cyber.Application.Exceptions;
using Cyber.Application.Messages.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Enums;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.ChangePassword;

internal class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEnumerable<IPasswordPolicy> _passwordPolicies;
    private readonly IPreviousPasswordsRepository _previousPasswordsRepository;
    private readonly IMessageBroker _messageBroker;

    public ChangePasswordHandler(IUsersRepository usersRepository, IEnumerable<IPasswordPolicy> passwordPolicies,
        IPreviousPasswordsRepository previousPasswordsRepository, IMessageBroker messageBroker)
    {
        _usersRepository = usersRepository;
        _passwordPolicies = passwordPolicies;
        _previousPasswordsRepository = previousPasswordsRepository;
        _messageBroker = messageBroker;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        if (!user.Password.IsMatch(request.OldPassword))
            throw new IncorrectCredentialsException($"Incorrect old password: {request.OldPassword}");

        var previousPasswords = await _previousPasswordsRepository.GetPreviousUserPasswords(request.UserId);
        var oldPassword = user.Password;

        await user.ChangePassword(request.NewPassword, _passwordPolicies, previousPasswords);

        await _previousPasswordsRepository.AddPassword(oldPassword, user.UserId);
        await _usersRepository.Update(user);

        await _messageBroker.Send(new UserPasswordChanged(DateTime.UtcNow, user.Username, user.UserId));

        return Unit.Value;
    }
}