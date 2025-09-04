using Moq;
using WakeCommerce.Domain.Models;
using WakeCommerce.Service.Interfaces;
using WakeCommerce.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WakeCommerce.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly ProdutoController _controller;
        private readonly Mock<IProdutoService> _serviceMock;

        public ProdutoControllerTests()
        {
            _serviceMock = new Mock<IProdutoService>();
            _controller = new ProdutoController(_serviceMock.Object);
        }

        [Fact]
        public async Task ObterTodos_RetornaOkComProdutos()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Valor = 10, Estoque = 5 }
            };
            _serviceMock.Setup(s => s.ObterTodos()).ReturnsAsync(produtos);

            var resultado = await _controller.ObterTodos();

            var okResult = resultado as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(produtos);
        }

        [Fact]
        public async Task ObterPorId_RetornaProduto()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Teste", Valor = 10, Estoque = 5 };
            _serviceMock.Setup(s => s.ObterPorId(1)).ReturnsAsync(produto);

            var resultado = await _controller.ObterPorId(1);

            var okResult = resultado as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(produto);
        }

        [Fact]
        public async Task Criar_RetornaProdutoCriado()
        {
            var produto = new Produto { Nome = "Novo Produto", Valor = 10, Estoque = 5 };
            _serviceMock.Setup(s => s.Criar(produto)).ReturnsAsync(produto);

            var resultado = await _controller.Criar(produto);

            var okResult = resultado as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(produto);
        }

        [Fact]
        public async Task Atualizar_RetornaNoContent()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Atualizado", Valor = 10, Estoque = 5 };
            _serviceMock.Setup(s => s.Atualizar(1, produto)).Returns(Task.CompletedTask);

            var resultado = await _controller.Atualizar(1, produto);

            resultado.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Deletar_RetornaNoContent()
        {
            _serviceMock.Setup(s => s.Deletar(1)).ReturnsAsync(true);

            var resultado = await _controller.Deletar(1);

            resultado.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task BuscarPorNome_RetornaProdutos()
        {
            var produtos = new List<Produto> { new Produto { Id = 1, Nome = "Teste", Valor = 10, Estoque = 5 } };
            _serviceMock.Setup(s => s.BuscarPorNome("Teste")).ReturnsAsync(produtos);

            var resultado = await _controller.BuscarPorNome("Teste");

            var okResult = resultado as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(produtos);
        }

        [Fact]
        public async Task Ordenar_RetornaProdutosOrdenados()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "B Produto", Valor = 20, Estoque = 5 },
                new Produto { Id = 2, Nome = "A Produto", Valor = 10, Estoque = 10 }
            };
            _serviceMock.Setup(s => s.Ordenar("valor", false)).ReturnsAsync(produtos);

            var resultado = await _controller.Ordenar("valor");

            var okResult = resultado as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(produtos);
        }

        [Fact]
        public async Task AtualizarEstoque_RetornaProdutoAtualizado()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Estoque", Valor = 10, Estoque = 5 };
            _serviceMock.Setup(s => s.AtualizarEstoque(produto.Id, 10))
                        .ReturnsAsync(() => { produto.Estoque += 10; return produto; });

            var resultado = await _controller.AtualizarEstoque(produto.Id, 10);

            var okResult = resultado as OkObjectResult;
            var produtoAtualizado = okResult.Value as Produto;
            produtoAtualizado.Estoque.Should().Be(15);
        }
    }
}
