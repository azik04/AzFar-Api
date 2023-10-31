using Final.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Final.Domain.ViewModel.Stadiums
{
    public class StadiumViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string? Name { get; set; }

        [Display(Name = "Адресс")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string? Adress { get; set; }

        public List<StadiumPhotos>? StadiumPhotos { get; set; }
    }
}
