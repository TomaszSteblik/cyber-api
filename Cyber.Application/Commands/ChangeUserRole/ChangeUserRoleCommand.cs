using Cyber.Application.DTOs.Update;
using MediatR;

namespace Cyber.Application.Commands.ChangeUserRole;

public class ChangeUserRoleCommand : IRequest
{
    public Guid UserId { get; set; }
    public UserRole NewRole { get; set; }

    public ChangeUserRoleCommand(Guid userId, UserRole newRole)
    {
        UserId = userId;
        NewRole = newRole;
    }
}