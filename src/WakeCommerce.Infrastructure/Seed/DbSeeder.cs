using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WakeCommerce.Infrastructure.Data;
using WakeCommerce.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WakeCommerce.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static async Task MigrateAndSeedAsync(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<WakeCommerceDbContext>();

            // Apenas para banco real; InMemory n�o precisa migrar
            if (db.Database.IsRelational())
            {
                await db.Database.MigrateAsync();
            }

            // Seed: se n�o houver produtos, insere 5 produtos
            if (!await db.Produtos.AnyAsync())
            {
                var produtos = new List<Produto>
                {
                    new Produto { Nome = "Produto 1", Valor = 10.0M, Estoque = 5, Descricao = "Descri��o 1" },
                    new Produto { Nome = "Produto 2", Valor = 20.0M, Estoque = 10, Descricao = "Descri��o 2" },
                    new Produto { Nome = "Produto 3", Valor = 30.0M, Estoque = 15, Descricao = "Descri��o 3" },
                    new Produto { Nome = "Produto 4", Valor = 40.0M, Estoque = 20, Descricao = "Descri��o 4" },
                    new Produto { Nome = "Produto 5", Valor = 50.0M, Estoque = 25, Descricao = "Descri��o 5" }
                };

                db.Produtos.AddRange(produtos);
                await db.SaveChangesAsync();
            }
        }
    }
}
