namespace PataseLuxos.Models
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento? FormaPagamento { get; set; }

    }
}
