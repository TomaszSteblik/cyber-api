using Cyber.Application.Exceptions;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.ChangePassword;

internal class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEnumerable<IPasswordPolicy> _passwordPolicies;
    private readonly IPreviousPasswordsRepository _previousPasswordsRepository;

    public ChangePasswordHandler(IUsersRepository usersRepository, IEnumerable<IPasswordPolicy> passwordPolicies, 
        IPreviousPasswordsRepository previousPasswordsRepository)
    {
        _usersRepository = usersRepository;
        _passwordPolicies = passwordPolicies;
        _previousPasswordsRepository = previousPasswordsRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);
        
        var previousPasswords = await _previousPasswordsRepository.GetPreviousUserPasswords(request.UserId);
        var oldPassword = user.Password;
        
        user.ChangePassword(request.NewPassword, _passwordPolicies, previousPasswords);
        await _previousPasswordsRepository.AddPassword(oldPassword, user.UserId);
        await _usersRepository.Update(user);
        
        return Unit.Value;
    }
}