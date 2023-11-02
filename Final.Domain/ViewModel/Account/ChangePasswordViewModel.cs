using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Enter UserName")]
        public string? UserName { get; set; }


        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "Password must be greater than or equal to 6 characters")]
        public string? NewPassword { get; set; }
    }
}
