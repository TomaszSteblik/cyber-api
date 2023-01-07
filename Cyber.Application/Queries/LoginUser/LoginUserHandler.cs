using Cyber.Application.Exceptions;
using Cyber.Application.Messages.Outgoing;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using Cyber.Domain.Services;
using MediatR;

namespace Cyber.Application.Queries.LoginUser;

internal class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IUserPasswordExpirySettingRepository _userPasswordExpirySettingRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IUserLoginAttemptsBlockService _userLoginAttemptsBlockService;
    private readonly ICaptchaService _captchaService;

    public LoginUserHandler(IUsersRepository usersRepository, IJwtService jwtService,
        IUserPasswordExpirySettingRepository userPasswordExpirySettingRepository, IMessageBroker messageBroker,
        IUserLoginAttemptsBlockService userLoginAttemptsBlockService, ICaptchaService captchaService)
    {
        _usersRepository = usersRepository;
        _jwtService = jwtService;
        _userPasswordExpirySettingRepository = userPasswordExpirySettingRepository;
        _messageBroker = messageBroker;
        _userLoginAttemptsBlockService = userLoginAttemptsBlockService;
        _captchaService = captchaService;
    }

    public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        if (!await _captchaService.VerifyPuzzleCaptchaChallenge(request.CaptchaChallengeId))
            throw new CaptchaChallengeFailedException("Failed puzzle captcha");
        
        //find user by email
        var user = await _usersRepository.GetUserByUsername(request.Login);
        if (user is null)
            throw new IncorrectCredentialsException($"login: {request.Login}");

        await _userLoginAttemptsBlockService.CheckIfUserIsBlockedFromFailedLoginAttempts(user.UserId);

        //check if user password matches hashed pass
        if (user.Password.IsMatch(request.Password) is false)
        {
            await _userLoginAttemptsBlockService.RegisterFailedLoginAttempt(user.UserId);
            throw new IncorrectCredentialsException($"pass: {request.Password}");
        }

        user.CheckPasswordExpiryDate(await _userPasswordExpirySettingRepository.GetPasswordLifetimeForUserGuid(user.UserId));

        if (user.IsBlocked)
            throw new UserBlockedException(user.UserId);

        await _messageBroker.Send(new UserLoggedIn(DateTime.UtcNow, user.Username, user.UserId));

        //create and return jwt token containing userId and role
        return await _jwtService.GenerateTokenForUser(user);
    }
}