using AutoMapper;
using Cyber.Application.DTOs.Read;
using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Commands.UpdateUser;

public class UpdateUserInformationsHandler : IRequestHandler<UpdateUserInformationsCommand, GetUserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UpdateUserInformationsHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<GetUserDto> Handle(UpdateUserInformationsCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        user.ChangeInformations(request.Username, request.Email, request.FirstName, request.LastName);
        user.Validate();

        await _usersRepository.Update(user);

        return _mapper.Map<GetUserDto>(user);

    }
}