using AutoMapper;
using Cyber.Application.DTOs.Read;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Queries.GetUsers;

internal class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUserDto>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetUsersPage(request.PageIndex);
        return _mapper.Map<IEnumerable<GetUserDto>>(users);
    }
}