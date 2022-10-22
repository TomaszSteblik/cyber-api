using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cyber.Application.Services;
using Cyber.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cyber.Infrastructure.Services;

internal class JwtService : IJwtService
{
    private readonly string _secret;
    private readonly string _expiration;

    public JwtService(IConfiguration configuration)
    {
        _secret = configuration.GetSection("Jwt")["secret"];
        _expiration = configuration.GetSection("Jwt")["expirationTimeInMinutes"];
    }
    public string GenerateTokenForUser(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("UserId",user.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expiration)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}