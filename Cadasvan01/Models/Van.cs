using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadasvan01.Models
{
    public class Van
    {
        [Key]
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string? Foto { get; set; }
        public string MotoristaId { get; set; }

        [ForeignKey("MotoristaId")]
        public Usuario Motorista { get; set; }


    }
}
