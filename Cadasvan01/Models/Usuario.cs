using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Cadasvan01.Enums;

namespace Cadasvan01.Models
{
    public class Usuario : IdentityUser
    {
        public string? CaminhoImagemPerfil { get; set; }
        public string NomeCompleto { get; set; }
        [MaxLength(11)]
        public string CPF { get; set; }
        public int CidadeId { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string Endereco { get; set; }
        [Phone]
        [RegularExpression(@"^\(\d{2}\)\d{5}-\d{4}$", ErrorMessage = "Formato indicado: (00)00000-0000.")]
        public string Celular1 { get; set; }
        [Phone]
        public string? Celular2 { get; set; }
        [MaxLength(11)]
        public string? CNH { get; set; }
        [MaxLength(7)]
        public string Placa { get; set; }
        public UsuarioEnum Tipo { get; set; }
        // Foreign key for Motorista (if this user is an Aluno)
        public string? MotoristaId { get; set; }
        public virtual Usuario? Motorista { get; set; }
        // Collection of Alunos (if this user is a Motorista)
        public virtual ICollection<Usuario>? Alunos { get; set; }
        public virtual Cidade? Cidade { get; set; }
    }

}