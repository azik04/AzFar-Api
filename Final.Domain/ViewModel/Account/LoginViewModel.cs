using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        public int Phone { set; get; }
        public string Password { set; get; }
    }
}
