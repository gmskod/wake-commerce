using Microsoft.EntityFrameworkCore;
using WakeCommerce.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using WakeCommerce.Infrastructure.Interface;
using WakeCommerce.Domain.Models;

namespace WakeCommerce.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly WakeCommerceDbContext _context;

        public ProdutoRepository(WakeCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Produto>> GetAll() =>
            await _context.Produtos.ToListAsync();

        public async Task<Produto> GetById(int id) =>
            await _context.Produtos.FindAsync(id);

        public async Task<Produto> Update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            return await _context.Produtos
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetOrdenados(string campo, bool desc = false)
        {
            IQueryable<Produto> query = _context.Produtos;

            query = campo.ToLower() switch
            {
                "nome" => desc ? query.OrderByDescending(p => p.Nome) : query.OrderBy(p => p.Nome),
                "estoque" => desc ? query.OrderByDescending(p => p.Estoque) : query.OrderBy(p => p.Estoque),
                "valor" => desc ? query.OrderByDescending(p => p.Valor) : query.OrderBy(p => p.Valor),
                _ => query.OrderBy(p => p.Id)
            };

            return await query.ToListAsync();
        }

        public async Task<Produto> AtualizarEstoque(int id, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return null;

            produto.Estoque += quantidade; // adiciona a quantidade (pode ser negativo para reduzir)
            await _context.SaveChangesAsync();
            return produto;
        }
    }
}
