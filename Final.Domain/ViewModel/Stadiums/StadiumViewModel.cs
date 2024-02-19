using Final.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Stadiums
{
    public class StadiumViewModel
    {

        public string? Name { get; set; }
        public string? Adress { get; set; }
        public List<IFormFile> StadiumPhoto { get; set; }
        public string StadiumLocation { get; set; }
        public string StadiumNumber { get; set; }
    }
}
