using Microsoft.AspNetCore.Mvc;
using WakeCommerce.Service.Interfaces;
using WakeCommerce.Domain.Models;

namespace WakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos() =>
            Ok(await _service.ObterTodos());

        /// <summary>
        /// Retorna um produto específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser retornado.</param>
        /// <returns>Produto encontrado ou NotFound caso não exista.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await _service.ObterPorId(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados.
        /// </summary>
        /// <param name="produto">Objeto do produto a ser criado.</param>
        /// <returns>Produto criado com sucesso.</returns>
        [HttpPost]
        public async Task<IActionResult> Criar(Produto produto) =>
            Ok(await _service.Criar(produto));

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produto">Objeto do produto com os novos dados.</param>
        /// <returns>NoContent se atualizado, BadRequest se o ID não corresponder.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Produto produto)
        {
            try
            {
                await _service.Atualizar(id, produto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Remove um produto do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>NoContent se removido, NotFound se não existir.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var resultado = await _service.Deletar(id);
            if (!resultado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Buscar produtos pelo nome
        /// </summary>
        /// <param name="nome">Parte ou todo o nome do produto</param>
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            var produtos = await _service.BuscarPorNome(nome);
            return Ok(produtos);
        }

        /// <summary>
        /// Ordenar produtos por um campo (nome, estoque, valor)
        /// </summary>
        /// <param name="campo">Campo para ordenação: nome, estoque, valor</param>
        /// <param name="desc">true para ordem decrescente</param>
        [HttpGet("ordenar")]
        public async Task<IActionResult> Ordenar([FromQuery] string campo, [FromQuery] bool desc = false)
        {
            var produtos = await _service.Ordenar(campo, desc);
            return Ok(produtos);
        }

        /// <summary>
        /// Atualiza apenas o estoque de um produto.
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="quantidade">Quantidade a adicionar (ou remover se negativo)</param>
        [HttpPatch("{id}/estoque")]
        public async Task<IActionResult> AtualizarEstoque(int id, [FromQuery] int quantidade)
        {
            var produto = await _service.AtualizarEstoque(id, quantidade);
            if (produto == null) return NotFound();
            return Ok(produto);
        }
    }
}
