using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        [MaxLength(110)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }


        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
