using Automarket.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Final.Domain.Entity;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Не указано имя")]
    [MaxLength(10)]
    public string Name { get; set; }
    public int Phone { get; set; }
    [JsonIgnore] public string Password { get; set; }
    public int RoleId { get; set; }
}
