using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Cadasvan01.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace Cadasvan01.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; }

        [MaxLength(11)]
        public string CPF { get; set; }

        public int CidadeId { get; set; }


        public string Endereco { get; set; }

        [MaxLength(11)]
        public string? CNH { get; set; }

        [MaxLength(7)]
        public string Placa { get; set; }
        //[Phone]
        // [DataType(DataType.PhoneNumber)]
        //public string Telefone { get; set; }
        public UsuarioEnum Tipo { get; set; }

       // [ForeignKey("CidadeId")]
        public virtual Cidade? Cidade { get; set; }

        //public virtual ICollection<ConfirmacaoDePresenca>? ConfirmadosAluno { get; set; }
        //public virtual ICollection<ConfirmacaoDePresenca>? ConfirmadosMotorista { get; set; }

    }
}