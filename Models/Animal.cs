namespace PataseLuxos.Models
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public string TipoAnimal { get; set; }
        public string NomeAnimal { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
