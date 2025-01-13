using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Domain.Interfaces;

namespace CatalogoProduto.Infra.Repositories
{
    public class DepartamentoRepository : IDepartamento
    {
        public Task<List<Departamento>> GetDepartamentos()
        {
            var departamentos = new List<Departamento>
            {
               new Departamento { Codigo = "010", Descricao = "BEBIDAS" },
               new Departamento { Codigo = "020", Descricao = "CONGELADOS" },
               new Departamento { Codigo = "030", Descricao = "LATICINIOS" },
               new Departamento { Codigo = "040", Descricao = "VEGETAIS" }
            };

            return Task.FromResult(departamentos);  
        }

    }
}
