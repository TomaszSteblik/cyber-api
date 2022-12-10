using AutoMapper;
using Cyber.Application.Exceptions;
using Cyber.Application.Messages.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Enums;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.ChangeUserRole;

internal class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IMessageBroker _messageBroker;

    public ChangeUserRoleHandler(IUsersRepository usersRepository, IMapper mapper, IMessageBroker messageBroker)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _messageBroker = messageBroker;
    }


    public async Task<Unit> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        var oldRole = user.Role;
        user.Role = (UserRole)request.NewRole;
        await _usersRepository.Update(user);
        await _messageBroker.Send(new UserRoleChanged(DateTime.Now, user.Username, user.UserId,
            (DTOs.Read.UserRole)oldRole, (DTOs.Read.UserRole)user.Role));
        return Unit.Value;
    }
}