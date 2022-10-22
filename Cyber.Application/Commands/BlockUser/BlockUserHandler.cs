using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.BlockUser;

internal class BlockUserHandler : IRequestHandler<BlockUserCommand>
{
    private readonly IUsersRepository _usersRepository;

    public BlockUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        if (request.Block)
            user.Block();
        else
            user.Unblock();

        await _usersRepository.Update(user);

        return Unit.Value;
    }
}