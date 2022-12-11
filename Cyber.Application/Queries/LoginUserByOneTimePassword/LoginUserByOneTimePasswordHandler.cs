using Cyber.Application.Commands.GenerateOneTimePassword;
using Cyber.Application.Exceptions;
using Cyber.Application.Messages.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using MediatR;

namespace Cyber.Application.Queries.LoginUserByOneTimePassword;

public class LoginUserByOneTimePasswordHandler : IRequestHandler<LoginUserByOneTimePasswordQuery, string>
{

    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IMessageBroker _messageBroker;
    private readonly IUserLoginAttemptsBlockService _userLoginAttemptsBlockService;
    private readonly IOneTimePasswordRepository _oneTimePasswordRepository;
    private readonly IOneTimePasswordCalculatorService _oneTimePasswordCalculatorService;
    private readonly IMediator _mediator;

    public LoginUserByOneTimePasswordHandler(IUserLoginAttemptsBlockService userLoginAttemptsBlockService,
        IMessageBroker messageBroker, IJwtService jwtService, IUsersRepository usersRepository,
        IOneTimePasswordRepository oneTimePasswordRepository, IOneTimePasswordCalculatorService oneTimePasswordCalculatorService,
        IMediator mediator)
    {
        _userLoginAttemptsBlockService = userLoginAttemptsBlockService;
        _messageBroker = messageBroker;
        _jwtService = jwtService;
        _usersRepository = usersRepository;
        _oneTimePasswordRepository = oneTimePasswordRepository;
        _oneTimePasswordCalculatorService = oneTimePasswordCalculatorService;
        _mediator = mediator;
    }

    public async Task<string> Handle(LoginUserByOneTimePasswordQuery request, CancellationToken cancellationToken)
    {
        //find user by email
        var user = await _usersRepository.GetUserByUsername(request.Login);
        if (user is null)
            throw new IncorrectCredentialsException($"login: {request.Login}");

        await _userLoginAttemptsBlockService.CheckIfUserIsBlockedFromFailedLoginAttempts(user.UserId);

        //check if user password matches

        var x = await _oneTimePasswordRepository.GetXValue(user.UserId);
        await _oneTimePasswordRepository.RemoveXValue(user.UserId);
        var a = request.Login.Length;
        var serverPassword = _oneTimePasswordCalculatorService.CalculateOneTimePassword(x, a);

        if (Math.Abs(serverPassword - request.Password) > 0.1)
        {
            await _userLoginAttemptsBlockService.RegisterFailedLoginAttempt(user.UserId);
            await _mediator.Send(new GenerateOneTimePasswordCommand(user.UserId), cancellationToken);
            throw new IncorrectCredentialsException($"pass: {request.Password}");
        }

        if (user.IsBlocked)
            throw new UserBlockedException(user.UserId);

        await _messageBroker.Send(new UserLoggedIn(DateTime.UtcNow, user.Username, user.UserId));

        //create and return jwt token containing userId and role
        return await _jwtService.GenerateTokenForUser(user);
    }
}