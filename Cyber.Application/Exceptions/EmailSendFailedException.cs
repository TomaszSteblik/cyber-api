using System.Net;

namespace Cyber.Application.Exceptions;

public class EmailSendFailedException : ApplicationException
{
    public EmailSendFailedException(string message) : base(message)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    public override string ErrorCode => "email_send_failed";
}