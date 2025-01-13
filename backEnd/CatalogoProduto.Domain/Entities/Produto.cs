using System.ComponentModel.DataAnnotations;

namespace CatalogoProduto.Domain.Entities
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório.")]
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string Descricao { get; set; }

     
        [Required(ErrorMessage = "Os departamentos são obrigatórios.")]
        public Departamento Departamentos { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Status { get; set; }
    }
}
