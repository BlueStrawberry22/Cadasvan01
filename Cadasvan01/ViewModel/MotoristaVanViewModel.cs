using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cadasvan01.ViewModel
{
    public class MotoristaVanViewModel
    {
        [Required]
        [Display(Name = "Modelo da Van")]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Cor da Van")]
        public string Cor { get; set; }

        [Display(Name = "Placa da Van")]
        public string Placa { get; set; }

        [Display(Name = "Foto da Van")]
        public IFormFile Foto { get; set; }
    }
}
