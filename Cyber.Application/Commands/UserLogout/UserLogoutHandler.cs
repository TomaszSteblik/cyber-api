using Cyber.Application.Exceptions;
using Cyber.Application.Messeges.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.UserLogout;

public class UserLogoutHandler : IRequestHandler<UserLogoutCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMessageBroker _messageBroker;

    public UserLogoutHandler(IUsersRepository usersRepository, IMessageBroker messageBroker)
    {
        _usersRepository = usersRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task<Unit> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        await _messageBroker.Send(new UserLoggedOut(DateTime.UtcNow, user.Username, user.UserId));
        
        return Unit.Value;
    }
}