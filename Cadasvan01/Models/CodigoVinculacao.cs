using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadasvan01.Models
{
    public class CodigoVinculacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(8)]
        public string Codigo { get; set; }

        [Required]
        public string MotoristaId { get; set; }

        [ForeignKey("MotoristaId")]
        public virtual Usuario Motorista { get; set; }
    }
}
