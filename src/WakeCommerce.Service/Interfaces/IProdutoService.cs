using WakeCommerce.Domain.Models;

namespace WakeCommerce.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto?> ObterPorId(int id);
        Task<Produto> Criar(Produto produto);
        Task Atualizar(int id, Produto produto);
        Task<bool> Deletar(int id);
        Task<IEnumerable<Produto>> BuscarPorNome(string nome);
        Task<IEnumerable<Produto>> Ordenar(string campo, bool desc = false);
        Task<Produto> AtualizarEstoque(int id, int quantidade);

    }
}