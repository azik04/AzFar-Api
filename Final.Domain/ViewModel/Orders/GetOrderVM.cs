using Final.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Final.Domain.ViewModel.Orders;

public class GetOrderVM
{
    public long Id { get; set; }
    public string StadiumId { get; set; }
    public string OrderTimeId { get; set; }
    public DateTime DateCreated { get; set; }
    public string FullName { get; set; }
    public string StadiumAdress { get; set; }
    public string StadiumPhoto { get; set; }

}
