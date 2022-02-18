using ConfiguracaoArquitetura.api.Business.Entities;

namespace ConfiguracaoArquitetura.api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();

        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}
