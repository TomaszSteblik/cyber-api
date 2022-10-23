namespace Cyber.Domain.Repositories;

public interface IUserPasswordExpirySettingRepository
{
    Task<uint> GetPasswordLifetimeForUserGuid(Guid userId);
    Task SetPasswordLifetimeForUserGuid(Guid guid, uint days);
}