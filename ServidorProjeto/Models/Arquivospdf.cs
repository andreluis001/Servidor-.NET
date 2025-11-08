namespace ServidorProjeto.Models
{
    public class ArquivoPdf
    {
        public int Id { get; set; }
        public string Nome { get; set; }       // Nome do arquivo ou link

        public ArquivoPdf() { }
        public ArquivoPdf(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
