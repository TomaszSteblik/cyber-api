using Cyber.Domain.Services;
using Cyber.Infrastructure.Exceptions;

namespace Cyber.Infrastructure.Repositories;

public class OneTimePasswordRepositoryInMemory : IOneTimePasswordRepository
{
    private readonly Dictionary<Guid, int> _xValues;


    public OneTimePasswordRepositoryInMemory()
    {
        _xValues = new Dictionary<Guid, int>();
    }

    public Task AddXValue(Guid userId, int xValue)
    {
        var success = _xValues.TryAdd(userId, xValue);
        if (success)
            return Task.CompletedTask;

        _xValues.Remove(userId);
        _xValues.Add(userId, xValue);

        return Task.CompletedTask;
    }

    public Task RemoveXValue(Guid userId)
    {
        _xValues.Remove(userId);
        return Task.CompletedTask;
    }

    public Task<int> GetXValue(Guid userId)
    {
        var success = _xValues.TryGetValue(userId, out var value);
        if (success)
            return Task.FromResult(value);
        throw new OneTimePasswordNotGeneratedException(userId);
    }
}