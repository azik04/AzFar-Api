using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Enter Username")]
    [MaxLength(20, ErrorMessage = "The Username must be less than 20 characters long")]
    [MinLength(3, ErrorMessage = "The Username must be longer than 3 characters")]
    public string? Username { get; set; }


    [Required(ErrorMessage = "Enter Phone")]
    [MaxLength(20, ErrorMessage = "The Phone must be less than 20 characters long")]
    [MinLength(3, ErrorMessage = "The Phone must be longer than 3 characters")]
    public string? Phone { get; set; }


    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Enter Password")]
    [MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
    public string? Password { get; set; }


    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Repeet Password")]
    [Compare("Password", ErrorMessage = "Password mismatch")]
    public string? PasswordConfirm { get; set; }
}
