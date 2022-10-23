using AutoMapper;
using Cyber.Domain.Repositories;
using Cyber.Infrastructure.DAOs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public class PasswordPoliciesRepository : MongoRepositoryBase<PasswordPolicyStatus>, IPasswordPoliciesRepository
{
    public PasswordPoliciesRepository(IMongoClient mongoClient) : base(mongoClient)
    {
        
    }

    public async Task<bool> GetEnabledStatusByPolicyKey(string key, Guid userId)
    {
        var statusCursor = await GetCollection().FindAsync(status => status.Key == key && status.UserId == userId.ToString());
        var status = await statusCursor.FirstOrDefaultAsync();
        
        return status is null || status.Status;
    }

    public async Task SaveEnabledStatus(string key, bool status, Guid userId)
    {
        var update = Builders<PasswordPolicyStatus>.Update.Set(nameof(PasswordPolicyStatus.Key), key).Set(nameof(PasswordPolicyStatus.Status), status);
        var options = new UpdateOptions {IsUpsert = true};
        await GetCollection().UpdateOneAsync(
            policyStatus => policyStatus.Key == key && policyStatus.UserId == userId.ToString(), 
            update, 
            options);
    }
}