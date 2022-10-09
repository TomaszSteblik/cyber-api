using System.Net;

namespace Cyber.Domain.Exceptions;

public class PasswordAlreadyUsedException : DomainException
{
    public PasswordAlreadyUsedException(string password, string userId) :
        base($"This password({password}) already was used by this user({userId})")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotAcceptable;
    public override string ErrorCode => "password_already_used";
}