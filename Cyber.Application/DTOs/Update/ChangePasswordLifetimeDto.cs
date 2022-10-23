namespace Cyber.Application.DTOs.Update;

public class ChangePasswordLifetimeDto
{
    public Guid UserId { get; set; }
    public uint ExpireTimeInDays { get; set; }
}