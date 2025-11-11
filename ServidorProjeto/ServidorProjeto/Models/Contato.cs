namespace ServidorProjeto.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}