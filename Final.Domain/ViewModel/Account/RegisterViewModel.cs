using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Account;

public class RegisterViewModel
{
    public string Name { set; get; }
    public int Phone { set; get; }
    public string Password { set; get; }
    public int RoleId { get; set; }
}
