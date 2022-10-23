using AutoMapper;
using Cyber.Domain.Repositories;
using Cyber.Infrastructure.DAOs;
using MongoDB.Bson;
using MongoDB.Driver;
using UserPassword = Cyber.Domain.ValueObjects.UserPassword;

namespace Cyber.Infrastructure.Repositories;

public class PreviousPasswordsRepository : MongoRepositoryBase<PreviousPassword>, IPreviousPasswordsRepository
{
    private readonly IMapper _mapper;

    public PreviousPasswordsRepository(IMongoClient mongoClient, IMapper mapper) : base(mongoClient)
    {
        _mapper = mapper;
    }
    public async Task<IEnumerable<UserPassword>> GetPreviousUserPasswords(Guid userId)
    {
        var cursor = await GetCollection().FindAsync(x => x.UserId == userId);
        var passwords = cursor.ToEnumerable().Select(x => x.Password);
        return _mapper.Map<IEnumerable<UserPassword>>(passwords);
    }

    public async Task AddPassword(UserPassword oldPassword, Guid userId)
    {
        var password = _mapper.Map<DAOs.UserPassword>(oldPassword);
        await GetCollection().InsertOneAsync(new PreviousPassword
        {
            Id = new ObjectId(),
            Password = password,
            UserId = userId
        });
    }
}