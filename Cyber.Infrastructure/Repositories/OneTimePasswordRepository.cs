using Cyber.Domain.Services;
using Cyber.Infrastructure.DAOs;
using Cyber.Infrastructure.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class OneTimePasswordRepository : MongoRepositoryBase<OneTimePassword>, IOneTimePasswordRepository
{
    public OneTimePasswordRepository(IMongoClient mongoClient) : base(mongoClient)
    {
    }

    public async Task AddXValue(Guid userId, int xValue)
    {
        var oneTimePassword = new OneTimePassword()
        {
            Id = new ObjectId(),
            X = xValue,
            UserId = userId
        };
        await GetCollection().InsertOneAsync(oneTimePassword);
    }

    public async Task RemoveXValue(Guid userId)
    {
        await GetCollection().DeleteManyAsync(x => x.UserId == userId);
    }

    public async Task<int> GetXValue(Guid userId)
    {
        var cursor = await GetCollection().FindAsync(x => x.UserId == userId);
        var oneTimePassword = await cursor.FirstOrDefaultAsync();
        if (oneTimePassword is null)
            throw new OneTimePasswordNotGeneratedException(userId);
        return oneTimePassword.X;
    }
}