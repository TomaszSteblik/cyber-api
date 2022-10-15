using Cyber.Application.DTOs.Read;
using MediatR;

namespace Cyber.Application.Commands.UpdateUser;

public class UpdateUserInformationsCommand : IRequest<GetUserDto>
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public UpdateUserInformationsCommand(Guid userId, string username, string firstName, string lastName, string email)
    {
        UserId = userId;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}