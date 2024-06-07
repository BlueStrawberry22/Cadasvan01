using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.Models
{
    public class Presenca
    {
        public int PresencaId { get; set; }


        public string UsuarioId { get; set; }
        public string MotoristaId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataViagem { get; set; }
        public bool ConfirmaIda { get; set; }
        public bool ConfirmaVolta { get; set; }


        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Motorista { get; set; }


    }
}
