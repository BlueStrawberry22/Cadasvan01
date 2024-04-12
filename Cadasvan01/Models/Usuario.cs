using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Cadasvan01.Enums;

namespace Cadasvan01.Models
{
        public class Usuario : IdentityUser
        {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }

        [Phone]
        public string Telefone { get; set; }
        public UsuarioEnum Tipo { get; set; }
    }
    }