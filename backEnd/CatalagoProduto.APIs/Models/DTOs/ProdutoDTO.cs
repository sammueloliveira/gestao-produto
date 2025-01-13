using CatalogoProduto.Domain.Entities;

namespace CatalogoProduto.APIs.Models.DTOs
{
    public class ProdutoDTO
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Departamento Departamentos { get; set; }
        public decimal Preco { get; set; }
        public bool Status { get; set; }
      
    }
}
