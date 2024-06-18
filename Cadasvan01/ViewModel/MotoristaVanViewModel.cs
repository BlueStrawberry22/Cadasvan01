using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cadasvan01.ViewModel
{
    public class MotoristaVanViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Cor { get; set; }

        [Required]
        public string Placa { get; set; }

        public IFormFile Foto { get; set; }

        public string CaminhoFotoVan { get; set; } // Caminho da foto existente
    }

}
