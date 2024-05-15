using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Cadasvan01.Models
{
    public class ConfirmacaoDePresenca
    {
        public int ConfirmacaoDePresencaID { get; set; }



        public string AlunoId { get; set; }
        public string MotoristaId { get; set; }
        public bool ConfirmadoPresencaIda { get; set; }
        public bool ConfirmadoPresencaVolta { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataViagemConfirmada {  get; set; }


        public virtual Usuario Aluno { get; set; }
        public virtual Usuario Motorista { get; set; }
    }
}
