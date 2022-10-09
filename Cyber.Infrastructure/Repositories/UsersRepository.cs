using Cyber.Domain.Entities;
using Cyber.Domain.Enums;
using Cyber.Domain.Repositories;

namespace Cyber.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private static List<User> _users;

    public UsersRepository()
    {
        _users = new List<User>
        {
            new User(
                "tsteblik", 
                "silnehaslo",
                "Tomasz",
                "Steblik",
                "ts055953@student.ath.edu.pl", 
                UserRole.User){UserId = Guid.Parse("7842869e-0d45-4880-a42a-52c91946ed0c")},
            new User(
                "rkulka",
                "slabehaslo",
                "Rafał",
                "Kulka",
                "rk055918@student.ath.edu.pl", 
                UserRole.User){UserId = Guid.Parse("24acdd21-6a87-4b3c-9a28-f67d7f3dd0be")},
            new User("ADMIN",
                "admin",
                "Admin",
                "Admin",
                "admin@cb.com",
                UserRole.Admin){UserId = Guid.Parse("3620194c-b1e0-4390-8272-9e4595b0856c")}
        };
    }
    
    public Task<User?> GetUserByEmail(string email)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.Email == email));
    }

    public Task<User?> GetUserById(Guid userId)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.UserId == userId));
    }

    public Task<User> Update(User user)
    {
        return Task.FromResult(user);
    }

    public Task<User> Add(User user)
    {
        _users.Add(user);
        return Task.FromResult(user);
    }
}