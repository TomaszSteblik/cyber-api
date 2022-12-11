using Cyber.Domain.Repositories;
using Cyber.Infrastructure.DAOs;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class ConfigRepository : MongoRepositoryBase<Config>, IConfigRepository
{
    private const int FailedLoginTimeoutInMinutesDefault = 15;
    private const int InactiveTimeoutInMinutesDefault = 15;
    private const int AllowedFailedLoginAttemptsCountDefault = 5;
    
    
    public ConfigRepository(IMongoClient mongoClient) : base(mongoClient)
    {
    }

    public async Task<int> GetFailedLoginTimeoutInMinutes()
    {
        var cursor = await GetCollection().FindAsync(x => x.Key == ConfigKey.FailedLoginTimeoutInMinutes);
        var config = await cursor.FirstOrDefaultAsync();
        return config?.Value ?? FailedLoginTimeoutInMinutesDefault;
    }

    public async Task SetFailedLoginTimeoutInMinutes(int value)
    {
        var update = Builders<Config>.Update.Set(nameof(Config.Key), ConfigKey.FailedLoginTimeoutInMinutes)
            .Set(nameof(Config.Value), value);
        var options = new UpdateOptions { IsUpsert = true };
        await GetCollection().UpdateOneAsync(
            config => config.Key == ConfigKey.FailedLoginTimeoutInMinutes,
            update,
            options);
    }

    public async Task<int> GetInactiveTimeoutInMinutes()
    {
        var cursor = await GetCollection().FindAsync(x => x.Key == ConfigKey.InactiveTimeoutInMinutes);
        var config = await cursor.FirstOrDefaultAsync();
        return config?.Value ?? InactiveTimeoutInMinutesDefault;
    }

    public async Task SetInactiveTimeoutInMinutes(int value)
    {
        var update = Builders<Config>.Update.Set(nameof(Config.Key), ConfigKey.InactiveTimeoutInMinutes)
            .Set(nameof(Config.Value), value);
        var options = new UpdateOptions { IsUpsert = true };
        await GetCollection().UpdateOneAsync(
            config => config.Key == ConfigKey.InactiveTimeoutInMinutes,
            update,
            options);
    }

    public async Task<int> GetAllowedFailedLoginAttemptsCount()
    {
        var cursor = await GetCollection().FindAsync(x => x.Key == ConfigKey.AllowedFailedLoginAttemptsCount);
        var config = await cursor.FirstOrDefaultAsync();
        return config?.Value ?? AllowedFailedLoginAttemptsCountDefault;
    }

    public async Task SetAllowedFailedLoginAttemptsCount(int value)
    {
        var update = Builders<Config>.Update.Set(nameof(Config.Key), ConfigKey.AllowedFailedLoginAttemptsCount)
            .Set(nameof(Config.Value), value);
        var options = new UpdateOptions { IsUpsert = true };
        await GetCollection().UpdateOneAsync(
            config => config.Key == ConfigKey.AllowedFailedLoginAttemptsCount,
            update,
            options);
    }
}