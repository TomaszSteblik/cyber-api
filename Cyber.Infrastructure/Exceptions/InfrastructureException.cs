using System.Net;

namespace Cyber.Infrastructure.Exceptions;

public abstract class InfrastructureException : SystemException
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string ErrorCode { get; }

    public InfrastructureException(string message) : base(message)
    {

    }
}