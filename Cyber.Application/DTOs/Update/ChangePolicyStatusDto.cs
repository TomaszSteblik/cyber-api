namespace Cyber.Application.DTOs.Update;

public class ChangePolicyStatusDto
{
    public Guid UserId { get; set; }
    public string Key { get; set; }

    public ChangePolicyStatusDto(Guid userId, string key)
    {
        UserId = userId;
        Key = key;
    }
}