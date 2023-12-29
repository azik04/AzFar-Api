using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.ViewModel.Orders;

public class OrderVM
{
    public long Id { get; set; }
    public string StadiumId { get; set; }
    public string OrderTimeId { get; set; }
    public DateTime DateCreated { get; set; }

    public string FullName { get; set; }

}
