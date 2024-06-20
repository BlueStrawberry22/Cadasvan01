using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadasvan01.Models
{
    public class Viagem
    {
        public int ViagemId { get; set; }

        [Required]
        public string MotoristaId { get; set; }

        [ForeignKey("MotoristaId")]
        public virtual Usuario Motorista { get; set; }

        [Required]
        public string Destino { get; set; }

        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public bool Ativa { get; set; }

        public string VanSelecionada { get; set; }
    }
}
