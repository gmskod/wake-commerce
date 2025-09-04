using WakeCommerce.Domain.Models;

namespace WakeCommerce.Infrastructure.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<Produto> Add(Produto produto);
        Task<Produto> Update(Produto produto);
        Task<bool> Delete(int id);
        Task<IEnumerable<Produto>> GetByNome(string nome);
        Task<IEnumerable<Produto>> GetOrdenados(string campo, bool desc = false);
        Task<Produto> AtualizarEstoque(int id, int quantidade);
    }
}
