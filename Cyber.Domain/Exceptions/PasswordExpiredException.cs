using System.Net;

namespace Cyber.Domain.Exceptions;

public class PasswordExpiredException : DomainException
{
    public PasswordExpiredException(string username) :
        base($"Users {username} password has expired. Please create new password")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public override string ErrorCode => "password_expired";
}