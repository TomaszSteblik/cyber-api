using System.Net;

namespace Cyber.Application.Exceptions;

public abstract class ApplicationException : SystemException
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string ErrorCode { get; }

    public ApplicationException(string message) : base(message)
    {
        
    }
}