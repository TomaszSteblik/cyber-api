using Cyber.Domain.Repositories;
using Cyber.Infrastructure.DAOs;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class UserPasswordExpirySettingRepository : MongoRepositoryBase<UserPasswordExpirySetting>, IUserPasswordExpirySettingRepository
{
    private const uint DefaultLifetime = 30;
    public UserPasswordExpirySettingRepository(IMongoClient mongoClient) : base(mongoClient)
    {

    }

    public async Task<uint> GetPasswordLifetimeForUserGuid(Guid userId)
    {
        var cursor = await GetCollection().FindAsync(x => x.UserId == userId);
        return (await cursor.FirstOrDefaultAsync())?.ExpiryTimeInDays ?? DefaultLifetime;
    }

    public async Task SetPasswordLifetimeForUserGuid(Guid userId, uint days)
    {
        var update = Builders<UserPasswordExpirySetting>.Update.Set(nameof(UserPasswordExpirySetting.ExpiryTimeInDays), days);
        var options = new UpdateOptions { IsUpsert = true };
        await GetCollection().UpdateOneAsync(
            setting => setting.UserId == userId,
            update,
            options);
    }
}