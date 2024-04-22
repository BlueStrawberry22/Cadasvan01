using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Cadasvan01.Models
{
    public class ConfirmacaoDePresenca
    {
        public int ConfirmacaoDePresencaID { get; set; }



        public string UsuarioId { get; set; }
        public string MotoristaId { get; set; }
        public bool ConfirmadoPresencaIda { get; set; }
        public bool ConfirmadoPresencaVolta { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataViagemConfirmada {  get; set; }


        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Motorista { get; set; }
    }
}
