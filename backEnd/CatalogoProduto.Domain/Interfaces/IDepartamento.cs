using CatalogoProduto.Domain.Entities;

namespace CatalogoProduto.Domain.Interfaces
{
    public interface IDepartamento 
    {
        Task<List<Departamento>> GetDepartamentos();
    }
}
