using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.Entity;

public class Stadium
{
    public long Id { get; set; }

    [Display(Name = "Название")]
    [Required(ErrorMessage = "Введите имя")]
    [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
    public string Name { get; set; }

    [Display(Name = "Описание")]
    [MinLength(50, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
    public string Adress { get; set; }

    public byte[]? Avatar { get; set; }
}
