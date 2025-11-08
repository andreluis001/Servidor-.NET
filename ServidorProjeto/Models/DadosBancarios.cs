namespace ServidorProjeto.Models
{
    public class DadosBancarios
    {
        public string NomeBanco { get; set; }
        public string CodigoBanco { get; set; } // ex: 237 para Bradesco
        public string Agencia { get; set; }
        public string Conta { get; set; }       // número da conta
        public string Metodo { get; set; }      // pix, boleto, cartão
    }
}
