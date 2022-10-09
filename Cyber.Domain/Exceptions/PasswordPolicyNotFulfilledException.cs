using System.Net;

namespace Cyber.Domain.Exceptions;

public class PasswordPolicyNotFulfilledException : DomainException
{
    public PasswordPolicyNotFulfilledException(string message) : base(message)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotAcceptable;
    public override string ErrorCode => "policy_not_fulfilled";
}