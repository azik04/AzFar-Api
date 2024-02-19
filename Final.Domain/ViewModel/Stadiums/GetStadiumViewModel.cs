using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Stadiums;

public class GetStadiumViewModel
{
    public string? Name { get; set; }
    public string? Adress { get; set; }
    public string StadiumPhotoName { get; set; }
    public string StadiumLocation { get; set; }
    public string StadiumNumber { get; set; }
}
