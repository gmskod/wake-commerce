using Microsoft.EntityFrameworkCore;
using WakeCommerce.Domain.Models;
using WakeCommerce.Infrastructure.Data;
using WakeCommerce.Infrastructure.Repositories;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace WakeCommerce.Tests.Repositories
{
    public class ProdutoRepositoryTests
    {
        private WakeCommerceDbContext ObterContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<WakeCommerceDbContext>()
                .UseInMemoryDatabase(databaseName: "TesteDb")
                .Options;
            return new WakeCommerceDbContext(options);
        }

        [Fact]
        public async Task Add_DeveAdicionarProduto()
        {
            using var context = ObterContextoEmMemoria();
            var repo = new ProdutoRepository(context);

            var produto = new Produto { Nome = "Produto Teste", Valor = 10, Estoque = 5 };
            var resultado = await repo.Add(produto);

            resultado.Id.Should().BeGreaterThan(0);
            (await context.Produtos.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task GetAll_DeveRetornarProdutos()
        {
            using var context = ObterContextoEmMemoria();
            context.Produtos.Add(new Produto { Nome = "Produto 1", Valor = 10, Estoque = 5 });
            await context.SaveChangesAsync();

            var repo = new ProdutoRepository(context);
            var resultado = await repo.GetAll();

            resultado.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByNome_DeveFiltrarProdutos()
        {
            using var context = ObterContextoEmMemoria();
            context.Produtos.AddRange(new List<Produto>
            {
                new Produto { Nome = "Teste 1", Valor = 10, Estoque = 5 },
                new Produto { Nome = "Outro", Valor = 15, Estoque = 3 }
            });
            await context.SaveChangesAsync();

            var repo = new ProdutoRepository(context);
            var resultado = await repo.GetByNome("Teste");

            resultado.Should().HaveCount(1);
        }

        [Fact]
        public async Task Ordenar_DeveOrdenarPorValor()
        {
            using var context = ObterContextoEmMemoria();
            context.Produtos.AddRange(new List<Produto>
            {
                new Produto { Nome = "B Produto", Valor = 20, Estoque = 5 },
                new Produto { Nome = "A Produto", Valor = 10, Estoque = 10 }
            });
            await context.SaveChangesAsync();

            var repo = new ProdutoRepository(context);
            var resultado = await repo.GetOrdenados("valor");

            resultado.Should().BeInAscendingOrder(p => p.Valor);
        }

        [Fact]
        public async Task AtualizarEstoque_DeveAtualizarSomenteEstoque()
        {
            using var context = ObterContextoEmMemoria();
            var produto = new Produto { Nome = "Produto Estoque", Valor = 10, Estoque = 5 };
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            var repo = new ProdutoRepository(context);
            var resultado = await repo.AtualizarEstoque(produto.Id, 10);

            resultado.Estoque.Should().Be(15);
        }
    }
}
