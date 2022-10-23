using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.ChangePasswordLifetime;

internal class ChangePasswordLifetimeHandler : IRequestHandler<ChangePasswordLifetimeCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserPasswordExpirySettingRepository _userPasswordExpirySettingRepository;

    public ChangePasswordLifetimeHandler(IUsersRepository usersRepository,
        IUserPasswordExpirySettingRepository userPasswordExpirySettingRepository)
    {
        _usersRepository = usersRepository;
        _userPasswordExpirySettingRepository = userPasswordExpirySettingRepository;
    }

    public async Task<Unit> Handle(ChangePasswordLifetimeCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        await _userPasswordExpirySettingRepository.SetPasswordLifetimeForUserGuid(request.UserId, request.ExpireTimeInDays);

        return Unit.Value;
    }
}