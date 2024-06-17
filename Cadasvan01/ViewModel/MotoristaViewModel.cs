using Cadasvan01.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.ViewModel
{
    public class MotoristaViewModel
    {

        [Display(Name = "Imagem de Perfil")]
        public IFormFile? ImagemPerfil { get; set; }

        public string? CaminhoImagemPerfil { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        [Display(Name = "Itinerário")]
        [Required(ErrorMessage = "Item obrigatório.")]
        [MaxLength(500, ErrorMessage = "O itinerário deve ter no máximo 500 caracteres.")]
        public string Itinerario { get; set; }

        [Phone]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string Celular1 { get; set; }

        [Phone]
        public string? Celular2 { get; set; }
        [MaxLength(11)]
        public string? CNH { get; set; }

        [MaxLength(7)]
        public string? Placa { get; set; }

        public UsuarioEnum Tipo { get; set; }

        // Campos padrões do Identity
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