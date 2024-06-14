using System;
using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.Models
{
    public class Avaliacao
    {
        public int AvaliacaoId { get; set; }

        [Range(0, 5)]
        public int AvaliacaoEstrelas { get; set; }

        public string Opiniao { get; set; }

        public string AlunoId { get; set; }
        public virtual Usuario Aluno { get; set; }

        public string MotoristaId { get; set; }
        public virtual Usuario Motorista { get; set; }

        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
    }
}
