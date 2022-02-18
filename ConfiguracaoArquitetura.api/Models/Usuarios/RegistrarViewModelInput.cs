using System.ComponentModel.DataAnnotations;

namespace ConfiguracaoArquitetura.api.Models.Usuarios
{
    public class RegistrarViewModelInput
    {
        [Required(ErrorMessage = "O Login e obrigatorio.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Email e obrigatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha e obrigatorio.")]
        public string Senha { get; set; }

    }
}
