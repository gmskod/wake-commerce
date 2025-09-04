using WakeCommerce.Domain.Models;
using WakeCommerce.Infrastructure.Interface;
using WakeCommerce.Service.Interfaces;

namespace WakeCommerce.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repositorio;

        public ProdutoService(IProdutoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Produto>> ObterTodos() =>
            await _repositorio.GetAll();

        public async Task<Produto?> ObterPorId(int id) =>
            await _repositorio.GetById(id);

        public async Task<Produto> Criar(Produto produto) =>
            await _repositorio.Add(produto);

        public async Task Atualizar(int id, Produto produto)
        {
            if (id != produto.Id)
                throw new ArgumentException("ID do produto não corresponde ao objeto enviado.");

            await _repositorio.Update(produto);
        }

        public async Task<bool> Deletar(int id) =>
            await _repositorio.Delete(id);

        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome) =>
            await _repositorio.GetByNome(nome);

        public async Task<IEnumerable<Produto>> Ordenar(string campo, bool desc = false) =>
            await _repositorio.GetOrdenados(campo, desc);

        public async Task<Produto> AtualizarEstoque(int id, int quantidade) =>
            await _repositorio.AtualizarEstoque(id, quantidade);
    }
}