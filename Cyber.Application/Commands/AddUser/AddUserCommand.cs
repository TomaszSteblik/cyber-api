using Cyber.Application.DTOs.Create;
using Cyber.Application.DTOs.Read;
using MediatR;

namespace Cyber.Application.Commands.AddUser;

public class AddUserCommand : IRequest<GetUserDto>
{
    public AddUserDto UserToAdd { get; set; }

    public AddUserCommand(AddUserDto userToAdd)
    {
        UserToAdd = userToAdd;
    }
}