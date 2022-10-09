namespace Cyber.Application.DTOs.Create;

public class AddUserDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public AddUserDto(string firstName, string lastName, string email, string username)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
    }
}