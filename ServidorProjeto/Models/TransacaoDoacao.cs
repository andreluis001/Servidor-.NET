using System;

namespace ServidorProjeto.Models
{
    public class TransacaoDoacao
    {
        public int Id { get; set; } // chave primária automática
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }

        public DadosBancarios DadosPagamento { get; set; } // será mapeado como owned entity
        public StatusTransacao? Status { get; set; }        // enum nullable

        public TransacaoDoacao()
        {
            DataTransacao = DateTime.Now;
        }
    }
}
