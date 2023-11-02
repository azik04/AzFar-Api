using Automarket.Domain.Enum;

namespace Final.Domain.Entity;

public class User
{
    public long id { get; set; }
    public string? Username { get; set; } = string.Empty;
    public string? PasswordHash { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public Role? Role { get; set; }
}
