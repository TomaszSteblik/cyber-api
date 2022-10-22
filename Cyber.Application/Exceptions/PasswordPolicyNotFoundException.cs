using System.Net;

namespace Cyber.Application.Exceptions;

public class PasswordPolicyNotFoundException : ApplicationException
{
    public PasswordPolicyNotFoundException(string key) : base($"Policy with key/name: {key} does not exist.")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public override string ErrorCode => "password_policy_not_found";
}