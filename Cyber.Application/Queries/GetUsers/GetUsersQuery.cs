using Cyber.Application.DTOs.Read;
using MediatR;

namespace Cyber.Application.Queries.GetUsers;

public class GetUsersQuery : IRequest<IEnumerable<GetUserDto>>
{
    public int PageIndex { get; set; }

    public GetUsersQuery(int pageIndex)
    {
        PageIndex = pageIndex;
    }
}