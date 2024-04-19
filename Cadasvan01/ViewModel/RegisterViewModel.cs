using Cadasvan01.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.ViewModel
{
    public class RegisterViewModel
    {

        //Campos personalizado do Usuario
        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        public string CPF { get; set; }

        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }
        public string Endereco { get; set; }

        [MaxLength(11)]
        public string? CNH { get; set; }

        [MaxLength(7)]
        public string Placa { get; set; }
        public UsuarioEnum Tipo { get; set; }

        //Campos padrões do Identity

        [Required(ErrorMessage = "Item obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmaSenha { get; set; }

    }
}
