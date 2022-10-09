using Cyber.Domain.Entities;

namespace Cyber.Application.Services;

public interface IJwtService
{
    string GenerateTokenForUser(User user);
}