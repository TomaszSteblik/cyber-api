using Cyber.Application.Exceptions;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using MediatR;

namespace Cyber.Application.Commands.GenerateOneTimePassword;

public class GenerateOneTimePasswordHandler : IRequestHandler<GenerateOneTimePasswordCommand>
{
    private readonly IOneTimePasswordRepository _oneTimePasswordRepository;
    private readonly Random _random;
    private readonly IUsersRepository _usersRepository;
    private readonly IMailingService _mailingService;

    public GenerateOneTimePasswordHandler(IOneTimePasswordRepository oneTimePasswordRepository, Random random,
        IUsersRepository usersRepository, IMailingService mailingService)
    {
        _oneTimePasswordRepository = oneTimePasswordRepository;
        _random = random;
        _usersRepository = usersRepository;
        _mailingService = mailingService;
    }

    public async Task<Unit> Handle(GenerateOneTimePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserById(request.UserId);
        if (user is null)
            throw new UserNotFoundException(request.UserId);

        var x = _random.Next();

        await _oneTimePasswordRepository.AddXValue(user.UserId, x);

        var emailStatus = await _mailingService.SendOneTimePasswordMail(user, x);
        if (!emailStatus)
        {
            throw new EmailSendFailedException($"Failed to send one time password email");
        }

        return Unit.Value;
    }
}