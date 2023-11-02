using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Username")]
        [MaxLength(35, ErrorMessage = "Username must be less than 35 characters long")]
        [MinLength(2, ErrorMessage = "Username must be more than 2 characters long")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
