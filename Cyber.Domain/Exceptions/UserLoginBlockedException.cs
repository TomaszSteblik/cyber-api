using System.Net;

namespace Cyber.Domain.Exceptions;

public class UserLoginBlockedException : DomainException
{
    public UserLoginBlockedException(int time) : base($"User blocked for {time} minutes because of too many failed login attempts.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public override string ErrorCode => "user_login_blocked_attempts";
}