using System.Net;

namespace Cyber.Domain.Exceptions;

public abstract class DomainException : SystemException
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string ErrorCode { get; }

    public DomainException(string message) : base(message)
    {
        
    }
}