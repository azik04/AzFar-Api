using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Accounts;

public class ChangePasswordViewModel
{
        [Required(ErrorMessage = "Укажите имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(5, ErrorMessage = "Пароль должен быть больше или равен 5 символов")]
        public string NewPassword { get; set; }
}

