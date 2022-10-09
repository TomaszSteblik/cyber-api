using System.Net;

namespace Cyber.Application.Exceptions;

internal class IncorrectCredentialsException : ApplicationException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public override string ErrorCode => "incorrect_credentials";

    public IncorrectCredentialsException(string cred) : base($"User with credential: {cred} not found")
    {
    }
}