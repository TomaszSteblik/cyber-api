using System.Net;

namespace Cyber.Domain.Exceptions;

internal class InvalidUserDataException : DomainException
{
    public InvalidUserDataException(string message) : base(message)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
    public override string ErrorCode => "invalid_user_data";
}