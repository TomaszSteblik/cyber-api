using MediatR;

namespace Cyber.Application.Queries.GetPasswordLifetime;

public class GetPasswordLifetimeQuery : IRequest<uint>
{
    public Guid UserId { get; set; }

    public GetPasswordLifetimeQuery(Guid userId)
    {
        UserId = userId;
    }
}