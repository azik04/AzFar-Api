using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Account;

public class RegisterViewModel
{
    [Required]
    [MinLength(1)]
    public string Name { set; get; }
    [Required]
    public int Phone { set; get; }
    [Required]
    public string Password { set; get; }
    public int RoleId { get; set; }
}
