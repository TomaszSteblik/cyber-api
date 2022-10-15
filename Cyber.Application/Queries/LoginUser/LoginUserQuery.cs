using System.ComponentModel;
using MediatR;

namespace Cyber.Application.Queries.LoginUser;

[Description("Request that returns JWT token for logged user")]
public class LoginUserRequest : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }

    public LoginUserRequest(string login, string password)
    {
        Login = login;
        Password = password;
    }
}