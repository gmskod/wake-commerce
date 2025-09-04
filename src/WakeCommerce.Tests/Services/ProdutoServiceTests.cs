using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WakeCommerce.Domain.Models;
using WakeCommerce.Infrastructure.Interface;
using WakeCommerce.Service;
using WakeCommerce.Service.Services;
using Xunit;

namespace WakeCommerce.Tests.Services
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _service;
        private readonly Mock<IProdutoRepository> _repositoryMock;

        public ProdutoServiceTests()
        {
            _repositoryMock = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeProdutos()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Valor = 10, Estoque = 5 },
                new Produto { Id = 2, Nome = "Produto 2", Valor = 20, Estoque = 10 }
            };
            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(produtos);

            var resultado = await _service.ObterTodos();

            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task BuscarPorNome_DeveRetornarProdutosCorretos()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Teste 1", Valor = 10, Estoque = 5 }
            };
            _repositoryMock.Setup(r => r.GetByNome("Teste")).ReturnsAsync(produtos);

            var resultado = await _service.BuscarPorNome("Teste");

            resultado.Should().HaveCount(1);
            resultado.Should().Contain(p => p.Nome.Contains("Teste"));
        }

        [Fact]
        public async Task Ordenar_DeveOrdenarProdutosPorValor()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "B Produto", Valor = 20, Estoque = 5 },
                new Produto { Id = 2, Nome = "A Produto", Valor = 10, Estoque = 10 }
            };
            _repositoryMock.Setup(r => r.GetOrdenados("valor", false)).ReturnsAsync(produtos);

            var resultado = await _service.Ordenar("valor");

            resultado.Should().ContainInOrder(produtos);
        }

        [Fact]
        public async Task AtualizarEstoque_DeveSomarQuantidade()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Estoque", Valor = 10, Estoque = 5 };
            _repositoryMock.Setup(r => r.AtualizarEstoque(produto.Id, 10))
                           .ReturnsAsync(() => { produto.Estoque += 10; return produto; });

            var resultado = await _service.AtualizarEstoque(produto.Id, 10);

            resultado.Estoque.Should().Be(15);
        }
    }
}
