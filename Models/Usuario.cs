namespace PataseLuxos.Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
