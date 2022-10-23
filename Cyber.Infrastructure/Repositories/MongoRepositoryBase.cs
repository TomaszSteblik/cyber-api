using Cyber.Infrastructure.DAOs;
using MongoDB.Driver;

namespace Cyber.Infrastructure.Repositories;

public abstract class MongoRepositoryBase<T> where T : IMongoModel
{
    private const string DatabaseName = "CyberDb";
    private readonly IMongoClient _mongoClient;

    protected MongoRepositoryBase(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    protected IMongoCollection<T> GetCollection()
    {
        var database = _mongoClient.GetDatabase(DatabaseName);
        var collection = database.GetCollection<T>(typeof(T).Name);
        return collection;
    }
}