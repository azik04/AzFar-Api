using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Accounts;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите имя")]
    [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
    public string Name { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; }

    [Required(ErrorMessage = "Укажите номер")]
    [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
    [MinLength(8, ErrorMessage = "Имя должно иметь длину больше 10 символов")]
    public string Phone { get; set; }

}
