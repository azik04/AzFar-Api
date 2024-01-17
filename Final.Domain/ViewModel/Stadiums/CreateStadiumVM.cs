using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Domain.ViewModel.Stadiums
{
    public class CreateStadiumVM
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string? Name { get; set; }

        [Display(Name = "Адресс")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string? Adress { get; set; }
        public string StadiumPhoto { get; set; }
    }
}
