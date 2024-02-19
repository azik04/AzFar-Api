using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        [MinLength(3, ErrorMessage = "Phine shoud be more than 3 symbols")]
        
        public string Phone { set; get; }
        [MinLength(3, ErrorMessage = "Password shoud be more than 8 symbols")]
        public string Password { set; get; }
    }
}
