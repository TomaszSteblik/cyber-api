using Cyber.Application.Exceptions;
using Cyber.Application.Services;
using Cyber.Domain.Repositories;
using MediatR;

namespace Cyber.Application.Queries.LoginUser;

internal class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IUserPasswordExpirySettingRepository _userPasswordExpirySettingRepository;

    public LoginUserHandler(IUsersRepository usersRepository, IJwtService jwtService,
        IUserPasswordExpirySettingRepository userPasswordExpirySettingRepository)
    {
        _usersRepository = usersRepository;
        _jwtService = jwtService;
        _userPasswordExpirySettingRepository = userPasswordExpirySettingRepository;
    }

    public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        //find user by email
        var user = await _usersRepository.GetUserByUsername(request.Login);
        if (user is null)
            throw new IncorrectCredentialsException($"login: {request.Login}");

        //check if user password matches hashed pass
        if (user.Password.IsMatch(request.Password) is false)
            throw new IncorrectCredentialsException($"pass: {request.Password}");

        user.CheckPasswordExpiryDate(await _userPasswordExpirySettingRepository.GetPasswordLifetimeForUserGuid(user.UserId));

        //create and return jwt token containing userId and role
        return _jwtService.GenerateTokenForUser(user);
    }
}