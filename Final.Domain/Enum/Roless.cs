using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Enum;

public enum Roless
{
    [Display(Name = "Пользователь")]
    User = 0,
    [Display(Name = "Модератор")]
    Moderator = 1,
    [Display(Name = "Админ")]
    Admin = 2,
}