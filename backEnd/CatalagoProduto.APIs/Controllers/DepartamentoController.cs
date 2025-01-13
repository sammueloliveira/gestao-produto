using CatalogoProduto.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoProduto.APIs.Controllers
{
    [Route("api/departamento")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamento _departamento;

        public DepartamentoController(IDepartamento departamento)
        {
            _departamento = departamento;
        }

        /// <summary>
        /// Lista os departamentos disponíveis.
        /// </summary>
        [HttpGet("departamentos")]
        public async Task<IActionResult> GetDepartamentos()
        {
            var departamentos = await _departamento.GetDepartamentos();

            return Ok(departamentos);
        }

    }
}
