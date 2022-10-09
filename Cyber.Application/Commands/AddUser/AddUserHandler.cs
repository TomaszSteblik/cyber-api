using AutoMapper;
using Cyber.Application.DTOs.Read;
using Cyber.Application.Services;
using Cyber.Domain.Entities;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Repositories;
using MediatR;
using UserRole = Cyber.Domain.Enums.UserRole;

namespace Cyber.Application.Commands.AddUser;

internal class AddUserHandler : IRequestHandler<AddUserCommand, GetUserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordGenerationService _passwordGenerationService;

    public AddUserHandler(IUsersRepository usersRepository, IMapper mapper, IPasswordGenerationService passwordGenerationService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _passwordGenerationService = passwordGenerationService;
    }
    
    public async Task<GetUserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var generatedPassword = _passwordGenerationService.GeneratePassword();
        var user = new User(request.UserToAdd.Username, generatedPassword, request.UserToAdd.FirstName,
            request.UserToAdd.LastName, request.UserToAdd.Email, UserRole.User);
        user.Validate();
        var addedUser = await _usersRepository.Add(user);
        //await _mailingService.SendPasswordEmail(user.Email, generatedPassword); TODO: Mailing service for first logon
        return _mapper.Map<GetUserDto>(addedUser);
    }
}