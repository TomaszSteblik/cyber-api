using Cyber.Application.DTOs.Read;
using MediatR;

namespace Cyber.Application.Queries.GetUserPolicies;

public class GetUserPasswordPoliciesQuery : IRequest<IEnumerable<GetPasswordPolicyDto>>
{
    public Guid UserId { get; set; }

    public GetUserPasswordPoliciesQuery(Guid userId)
    {
        UserId = userId;
    }
}