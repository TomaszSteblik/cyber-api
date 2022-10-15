using Cyber.Domain.Entities;

namespace Cyber.Domain.Repositories;

public interface IUsersRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(Guid userId);
    Task<User> Update(User user);
    Task<User> Add(User user);
    Task<IEnumerable<User>> GetUsersPage(int requestPageIndex);
}