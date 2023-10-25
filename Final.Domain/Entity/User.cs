using Automarket.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Final.Domain.Entity;

public class User
{
    public long Id { get; set; }
    public Role Role { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}
