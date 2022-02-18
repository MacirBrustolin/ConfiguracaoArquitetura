using ConfiguracaoArquitetura.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfiguracaoArquitetura.api.Configuration
{
    public class FactoryConfigurationDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=NOTEBOOK-MACIR\\SQLEXPRESS; Database=Curso;Trusted_Connection=true;MultipleActiveResultSets=True");

            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
