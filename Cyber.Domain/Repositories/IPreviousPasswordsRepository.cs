using Cyber.Domain.ValueObjects;

namespace Cyber.Domain.Repositories;

public interface IPreviousPasswordsRepository
{
    public Task<IEnumerable<UserPassword>> GetPreviousUserPasswords(Guid userId);
    Task AddPassword(UserPassword oldPassword, Guid userId);
}