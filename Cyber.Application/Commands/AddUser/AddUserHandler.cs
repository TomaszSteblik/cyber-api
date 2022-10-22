using AutoMapper;
using Cyber.Application.DTOs.Read;
using Cyber.Application.Exceptions;
using Cyber.Application.Services;
using Cyber.Domain.Entities;
using Cyber.Domain.Policies.PasswordPolicy;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using MediatR;
using UserRole = Cyber.Domain.Enums.UserRole;

namespace Cyber.Application.Commands.AddUser;

internal class AddUserHandler : IRequestHandler<AddUserCommand, GetUserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordGenerationService _passwordGenerationService;
    private readonly IMailingService _mailingService;

    public AddUserHandler(IUsersRepository usersRepository, IMapper mapper, IPasswordGenerationService passwordGenerationService,
        IMailingService mailingService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _passwordGenerationService = passwordGenerationService;
        _mailingService = mailingService;
    }

    public async Task<GetUserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var generatedPassword = _passwordGenerationService.GeneratePassword();
        var user = new User(request.UserToAdd.Username, generatedPassword, request.UserToAdd.FirstName,
            request.UserToAdd.LastName, request.UserToAdd.Email, UserRole.PasswordChangeRequired);
        user.Validate();
        var addedUser = await _usersRepository.Add(user);
        var emailStatus = await _mailingService.SendPasswordMail(user, generatedPassword);
        if (!emailStatus)
        {
            //TODO: Put mail into outbox
            throw new EmailSendFailedException($"Failed to send new password email");
        }
        return _mapper.Map<GetUserDto>(addedUser);
    }
}