namespace PataseLuxos.Models
{
    public class FormaPagamento
    {
        public Guid FormaPagamentoId { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string Cartao { get; set; }
        public string Validade { get; set; }
        public string CVV { get; set; }
        public string Senha { get; set; }
    }
}
