using System.Net;

namespace Cyber.Infrastructure.Exceptions;

public class OneTimePasswordNotGeneratedException : InfrastructureException
{
    public OneTimePasswordNotGeneratedException(Guid userId) : base($"One time pass for user with id {userId} not found")
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public override string ErrorCode => "one_time_pass_not_generated";
}