using Cyber.Domain.Repositories;
using Cyber.Domain.ValueObjects;

namespace Cyber.Infrastructure.Repositories;

public class PreviousPasswordsRepository : IPreviousPasswordsRepository
{
    private List<(Guid id, UserPassword pasword)> _passwords;

    public PreviousPasswordsRepository()
    {
        _passwords = new List<(Guid, UserPassword)>
        {
            (Guid.Parse("3620194c-b1e0-4390-8272-9e4595b0856c"), new UserPassword("Test123")),
            (Guid.Parse("7842869e-0d45-4880-a42a-52c91946ed0c"), new UserPassword("Kl123")),
            (Guid.Parse("24acdd21-6a87-4b3c-9a28-f67d7f3dd0be"), new UserPassword("Tost987"))
        };
    }
    public Task<IEnumerable<UserPassword>> GetPreviousUserPasswords(Guid userId)
    {
        return Task.FromResult(_passwords.Where(x => x.id == userId).Select(pair => pair.pasword));
    }

    public Task AddPassword(UserPassword oldPassword, Guid userId)
    {
        _passwords.Add((userId, oldPassword));
        return Task.CompletedTask;
    }
}