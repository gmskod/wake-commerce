namespace WakeCommerce.Domain.Models;
public class Produto
{
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
}