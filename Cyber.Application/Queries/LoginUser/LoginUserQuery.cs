using System.ComponentModel;
using MediatR;

namespace Cyber.Application.Queries.LoginUser;

[Description("Request that returns JWT token for logged user")]
public class LoginUserRequest : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string CaptchaChallengeId { get; set; }

    public LoginUserRequest(string login, string password, string captchaChallengeId)
    {
        Login = login;
        Password = password;
        CaptchaChallengeId = captchaChallengeId;
    }
}