namespace Cyber.Application.DTOs.Read;

public class ConfigDto
{
    public int InactiveTimeout { get; set; }
    public int AllowedLoginAttempts { get; set; }
    public int FailedLoginTimeout { get; set; }
}