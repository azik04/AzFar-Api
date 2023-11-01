namespace Final.Domain.Entity;

public class UserDto
{
    public long id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
