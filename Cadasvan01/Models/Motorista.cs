using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.Models
{
    public class Motorista : Usuario
    {
        //Classe específica só para as informações adicionais do motorista?
        [MaxLength(11)]
        public string? CNH { get; set; }

        [MaxLength(7)]
        public string? Placa { get; set; }
    }
}
