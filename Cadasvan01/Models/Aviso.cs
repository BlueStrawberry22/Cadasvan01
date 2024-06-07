using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.Models
{
    public class Aviso
    {
        public int AvisoId { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Conteudo { get; set; }

        public DateTime DataPublicacao { get; set; } = DateTime.Now;

        public string MotoristaId { get; set; }
        public virtual Usuario Motorista { get; set; }

        public bool Lido { get; set; } = false; // Adiciona a propriedade Lido
    }
}
