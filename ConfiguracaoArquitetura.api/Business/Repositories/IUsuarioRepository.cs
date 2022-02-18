using ConfiguracaoArquitetura.api.Business.Entities;

namespace ConfiguracaoArquitetura.api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Commit();
        Task<Usuario> ObterUsuarioAsync(string login);
    }
}
