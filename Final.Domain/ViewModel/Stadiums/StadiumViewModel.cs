using Final.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Stadiums
{
    public class StadiumViewModel
    {

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string? Name { get; set; }

        [Display(Name = "Адресс")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string? Adress { get; set; }
        public List<IFormFile> StadiumPhoto { get; set; }
    }
}
