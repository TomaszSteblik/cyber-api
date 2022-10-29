using System.Net;

namespace Cyber.Application.Exceptions;

public class UserBlockedException : ApplicationException
{
    public UserBlockedException(Guid userId) : base($"User with id: {userId} is blocked")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public override string ErrorCode => "user_blocked";
}