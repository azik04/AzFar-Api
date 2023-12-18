using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Orders;

public class OrderVM
{
    public long Id { get; set; }
    public string StadiumId { get; set; }
    public string OrderTimeId { get; set; }

    [Display(Name = "Дата создания")]
    public DateTime DateCreated { get; set; }

    [Display(Name = "Полное имя")]
    [MaxLength(50, ErrorMessage = "Полное имя должно иметь длину меньше 50 символов")]
    [MinLength(2, ErrorMessage = "Полное имя должно иметь длину больше 2 символов")]
    public string FullName { get; set; }

}
