using System.Drawing;

namespace Cadasvan01.Models
{
    public class Aviso
    {
        public int AvisoId { get; set; }


        public string Mensagem { get; set; }
        public DateTime DataDoAviso { get; set; }

        // public virtual Usuario Usuario { get; set; }
        // public virtual Usuario Motorista { get; set; }

    }
}
