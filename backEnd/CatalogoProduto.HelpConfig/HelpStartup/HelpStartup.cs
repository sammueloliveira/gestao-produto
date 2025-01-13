using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogoProduto.HelpConfig.HelpStartup
{
    public static class HelpStartup
     {
        public static void ConfigureScoped(IServiceCollection services)
        {
            // INTERFACE REPOSITORIO
           
            services.AddScoped<IDepartamento, DepartamentoRepository>();

            
        }
     }
}
