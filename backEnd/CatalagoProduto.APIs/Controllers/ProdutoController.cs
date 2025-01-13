using AutoMapper;
using CatalogoProduto.APIs.Models.DTOs;
using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Infra.Repositories; 
using Microsoft.AspNetCore.Mvc;

namespace CatalagoProduto.APIs.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(ProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os produtos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllAsync();

            if (produtos == null)
                produtos = new List<Produto>();

            return Ok(produtos);
        }

        /// <summary>
        /// Retorna os detalhes de um produto específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProdutoById(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null)
                return NotFound($"Produto com ID {id} não encontrado.");

            return Ok(produto);
        }

        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddProduto([FromBody] ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");


            if (produtoDTO.Departamentos == null || !_produtoRepository.ValidarDepartamento(produtoDTO.Departamentos.Codigo, produtoDTO.Departamentos.Descricao))
            {
                return BadRequest("Código do departamento inválido. Insira um dos seguintes departamentos com o código e descrição correspondentes: \n" +
                            "\"010\" - \"BEBIDAS\"\n" +
                            "\"020\" - \"CONGELADOS\"\n" +
                            "\"030\" - \"LATICÍNIOS\"\n" +
                            "\"040\" - \"VEGETAIS\"");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Id = Guid.NewGuid();

            await _produtoRepository.AddAsync(produto);

            return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Id }, produto);
        }


        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduto(Guid id, [FromBody] UpdateProdutoDTO updateProdutoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var produtoExistente = await _produtoRepository.GetByIdAsync(id);

            if (produtoExistente == null)
                return NotFound($"Produto com ID {id} não encontrado.");


            if (updateProdutoDTO.Departamentos == null || !_produtoRepository.ValidarDepartamento(updateProdutoDTO.Departamentos.Codigo, updateProdutoDTO.Departamentos.Descricao))
            {
                return BadRequest("Código do departamento inválido. Insira um dos seguintes departamentos com o código e descrição correspondentes: \n" +
                            "\"010\" - \"BEBIDAS\"\n" +
                            "\"020\" - \"CONGELADOS\"\n" +
                            "\"030\" - \"LATICÍNIOS\"\n" +
                            "\"040\" - \"VEGETAIS\"");
            }

            var produto = _mapper.Map<Produto>(updateProdutoDTO);
            produto.Id = id;

            await _produtoRepository.UpdateAsync(produto);

            return Ok(produto);
        }



        /// <summary>
        /// Exclui logicamente um produto.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null)
                return NotFound($"Produto com ID {id} não encontrado.");

            await _produtoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
