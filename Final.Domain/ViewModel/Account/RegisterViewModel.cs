using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Account;

public class RegisterViewModel
{
    [MinLength(3,ErrorMessage ="Name shoud be more than 3 symbols")]
    public string Name { set; get; }
    public int Phone { set; get; }
    [MinLength(3, ErrorMessage = "Password shoud be more than 8 symbols")]
    public string Password { set; get; }
    public int RoleId { get; set; }
}
