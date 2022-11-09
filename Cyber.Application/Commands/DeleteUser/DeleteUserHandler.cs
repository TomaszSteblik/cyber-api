using Cyber.Application.Exceptions;
using Cyber.Application.Messages.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.DeleteUser;

internal class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteUserHandler(IUsersRepository usersRepository, IMessageBroker messageBroker)
    {
        _usersRepository = usersRepository;
        _messageBroker = messageBroker;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        await _usersRepository.Delete(user.UserId);

        await _messageBroker.Send(new UserDeleted(DateTime.UtcNow, user.Username, user.UserId));

        return Unit.Value;
    }
}