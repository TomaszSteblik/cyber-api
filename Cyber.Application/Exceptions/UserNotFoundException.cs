using System.Net;

namespace Cyber.Application.Exceptions;

internal class UserNotFoundException : ApplicationException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public override string ErrorCode => "user_not_found";

    public UserNotFoundException(Guid guid) : base($"User with id: {guid} not found.")
    {

    }

    public UserNotFoundException(string email) : base($"User with email: {email} not found.")
    {

    }
}