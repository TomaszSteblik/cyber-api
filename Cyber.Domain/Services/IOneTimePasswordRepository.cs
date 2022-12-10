namespace Cyber.Domain.Services;

public interface IOneTimePasswordRepository
{
    Task AddXValue(Guid userId, int xValue);
    Task RemoveXValue(Guid userId);
    Task<int> GetXValue(Guid userId);
}