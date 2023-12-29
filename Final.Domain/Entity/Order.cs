using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.Entity;

public class Order
{
    public long Id { get; set; }
    public long StadiumId { get; set; }
    public long OrderTimeId { get; set; }
    public DateTime? DateCreated { get; set; }
    public int FullName { get; set; }

}
