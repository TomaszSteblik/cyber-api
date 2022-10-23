using AutoMapper;
using Cyber.Domain.Entities;
using Cyber.Domain.Enums;
using Cyber.Domain.Repositories;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class UsersRepository : MongoRepositoryBase<DAOs.User>, IUsersRepository
{
    private readonly IMapper _mapper;
    private const int EntriesPerPage = 100;

    public UsersRepository(IMongoClient mongoClient, IMapper mapper) : base(mongoClient)
    {
        _mapper = mapper;
    }

    private async Task PrepareDbIfEmpty()
    {
        if (await GetCollection().CountDocumentsAsync(FilterDefinition<DAOs.User>.Empty) > 0)
            return;

        var users = new List<User>
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
                UserRole.PasswordChangeRequired){UserId = Guid.Parse("24acdd21-6a87-4b3c-9a28-f67d7f3dd0be")},
            new User("ADMIN",
                "admin",
                "Admin",
                "Admin",
                "admin@cb.com",
                UserRole.Admin){UserId = Guid.Parse("3620194c-b1e0-4390-8272-9e4595b0856c")}
        };
        users[0].Password.CreatedAt = users[0].Password.CreatedAt.AddDays(-60);
        var usersDao = _mapper.Map<IEnumerable<DAOs.User>>(users);
        await GetCollection().InsertManyAsync(usersDao);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        await PrepareDbIfEmpty();

        var cursor = await GetCollection().FindAsync(user => user.Email == email);
        var user = await cursor.FirstOrDefaultAsync();
        return _mapper.Map<User>(user);
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        await PrepareDbIfEmpty();

        var cursor = await GetCollection().FindAsync(user => user.UserId == userId);
        var user = await cursor.FirstOrDefaultAsync();
        return _mapper.Map<User>(user);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        await PrepareDbIfEmpty();

        var cursor = await GetCollection().FindAsync(user => user.Username == username);
        var user = await cursor.FirstOrDefaultAsync();
        return _mapper.Map<User>(user);
    }

    public async Task<User> Update(User user)
    {
        await PrepareDbIfEmpty();

        var userDao = _mapper.Map<DAOs.User>(user);

        var update = Builders<DAOs.User>.Update
            .Set(nameof(DAOs.User.Email), userDao.Email)
            .Set(nameof(DAOs.User.Password), userDao.Password)
            .Set(nameof(DAOs.User.Role), userDao.Role)
            .Set(nameof(DAOs.User.Username), userDao.Username)
            .Set(nameof(DAOs.User.FirstName), userDao.FirstName)
            .Set(nameof(DAOs.User.IsBlocked), userDao.IsBlocked)
            .Set(nameof(DAOs.User.LastName), userDao.LastName);


        var added = await GetCollection().FindOneAndUpdateAsync(u => u.UserId == userDao.UserId, update);

        return _mapper.Map<User>(added);
    }

    public async Task<User> Add(User user)
    {
        await PrepareDbIfEmpty();

        var userToAdd = _mapper.Map<DAOs.User>(user);
        await GetCollection().InsertOneAsync(userToAdd);
        var cursor = await GetCollection().FindAsync(u => u.UserId == user.UserId);
        var addedUser = await cursor.FirstOrDefaultAsync();
        return _mapper.Map<User>(addedUser);
    }

    public async Task<IEnumerable<User>> GetUsersPage(int requestPageIndex)
    {
        await PrepareDbIfEmpty();

        var users = await GetCollection()
            .Find(FilterDefinition<DAOs.User>.Empty)
            .Skip(requestPageIndex * EntriesPerPage)
            .Limit((requestPageIndex + 1) * EntriesPerPage)
            .ToListAsync();

        return _mapper.Map<IEnumerable<User>>(users);
    }

    public async Task Delete(Guid userId)
    {
        await PrepareDbIfEmpty();

        await GetCollection().DeleteOneAsync(user => user.UserId == userId);
    }
}