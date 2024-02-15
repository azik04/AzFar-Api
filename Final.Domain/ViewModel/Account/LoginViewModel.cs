using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан номер")]
        public string Phone { set; get; }
        public string Password { set; get; }
    }
}
