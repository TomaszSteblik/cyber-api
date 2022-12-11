using Cyber.Domain.Entities;

namespace Cyber.Application.Services;

public interface IJwtService
{
    Task<string> GenerateTokenForUser(User user);
}