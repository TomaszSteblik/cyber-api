using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.DeleteUser;

internal class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        await _usersRepository.Delete(user.UserId);
        return Unit.Value;
    }
}