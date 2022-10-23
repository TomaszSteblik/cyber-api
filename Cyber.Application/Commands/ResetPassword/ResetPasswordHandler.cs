using Cyber.Application.Exceptions;
using Cyber.Application.Services;
using Cyber.Domain.Enums;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using Cyber.Domain.ValueObjects;
using MediatR;

namespace Cyber.Application.Commands.ResetPassword;

internal class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordGenerationService _passwordGenerationService;
    private readonly IMailingService _mailingService;

    public ResetPasswordHandler(IUsersRepository usersRepository, IPasswordGenerationService passwordGenerationService,
        IMailingService mailingService)
    {
        _usersRepository = usersRepository;
        _passwordGenerationService = passwordGenerationService;
        _mailingService = mailingService;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByEmail(request.UserEmail);
        if (user is null)
            throw new UserNotFoundException(request.UserEmail);

        var generatedPassword = _passwordGenerationService.GeneratePassword();

        user.Password = new UserPassword(generatedPassword);

        if (user.Role is UserRole.User)
            user.Role = UserRole.PasswordChangeRequired;

        var emailStatus = await _mailingService.SendPasswordMail(user, generatedPassword);
        if (!emailStatus)
        {
            throw new EmailSendFailedException($"Failed to send new password email");
        }

        await _usersRepository.Update(user);

        return Unit.Value;
    }
}