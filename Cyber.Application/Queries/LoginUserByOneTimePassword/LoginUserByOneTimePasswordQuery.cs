using MediatR;

namespace Cyber.Application.Queries.LoginUserByOneTimePassword;

public class LoginUserByOneTimePasswordQuery : IRequest<string>
{
    public string Login { get; set; }
    public double Password { get; set; }
    public string CaptchaChallengeId { get; set; }

    public LoginUserByOneTimePasswordQuery(string login, double password, string captchaChallengeId)
    {
        Login = login;
        Password = password;
        CaptchaChallengeId = captchaChallengeId;
    }

}