using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Item obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        public string CPF { get; set; }    
        public string Telefone { get; set; }
    }
}
