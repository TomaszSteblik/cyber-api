using Cyber.Domain.Repositories;
using Cyber.Infrastructure.DAOs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class LoginAttemptsRepository : MongoRepositoryBase<LoginAttempt>, ILoginAttemptsRepository
{
    public LoginAttemptsRepository(IMongoClient mongoClient) : base(mongoClient)
    {
    }

    public async Task AddFailedAttempt(Guid userId)
    {
        var attempt = new LoginAttempt()
        {
            Id = new ObjectId(),
            UserId = userId,
            AttemptTime = DateTime.Now
        };
        await GetCollection().InsertOneAsync(attempt);
    }

    public async Task<int> GetFailedAttemptsCountForUser(Guid userId)
    {
        var cursor = await GetCollection().FindAsync(x => x.UserId == userId);
        var count = cursor.ToEnumerable().Count();
        return count;
    }

    public async Task RemoveAttemptsOlderThan(DateTime time)
    {
        await GetCollection().FindOneAndDeleteAsync(x => x.AttemptTime <= time);
    }
}