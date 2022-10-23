using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Queries.GetPasswordLifetime;

internal class GetPasswordLifetimeHandler : IRequestHandler<GetPasswordLifetimeQuery, uint>
{
    private readonly IUserPasswordExpirySettingRepository _userPasswordExpirySettingRepository;
    private readonly IUsersRepository _usersRepository;

    public GetPasswordLifetimeHandler(IUserPasswordExpirySettingRepository userPasswordExpirySettingRepository,
        IUsersRepository usersRepository)
    {
        _userPasswordExpirySettingRepository = userPasswordExpirySettingRepository;
        _usersRepository = usersRepository;
    }

    public async Task<uint> Handle(GetPasswordLifetimeQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        return await _userPasswordExpirySettingRepository.GetPasswordLifetimeForUserGuid(request.UserId);
    }
}