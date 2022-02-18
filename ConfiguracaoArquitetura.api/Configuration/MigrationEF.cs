using ConfiguracaoArquitetura.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConfiguracaoArquitetura.api.Configuration
{
    public static class MigrationEF
    {
        public static IApplicationBuilder UseApplyMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var cursoDbContext = serviceScope.ServiceProvider.GetService<CursoDbContext>())
                {
                    var migracoesPendentes = cursoDbContext.Database.GetPendingMigrations();

                    if (migracoesPendentes.Count() == 0)
                    {
                        return app;
                    }

                    cursoDbContext.Database.Migrate();
                }
            }
            return app;
        }
    }
}
