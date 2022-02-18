using ConfiguracaoArquitetura.api.Models.Usuarios;

namespace ConfiguracaoArquitetura.api.Configuration
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
