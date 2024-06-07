using System.ComponentModel.DataAnnotations;

namespace Cadasvan01.ViewModel
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string Nome{ get; set; }


        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Item obrigatório.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Item obrigatório.")]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public string Endereco { get; set; }

        [MaxLength(11)]
        public string? CNH { get; set; }

        [MaxLength(7)]
        public string Placa { get; set; }

        [Display(Name = "Imagem de Perfil")]
        public IFormFile? ImagemPerfil { get; set; }

        public string CaminhoImagemPerfil { get; set; }
    }
}
