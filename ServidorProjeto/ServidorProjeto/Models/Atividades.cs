using System;
using System.Collections.Generic;

namespace ServidorProjeto.Models
{
    public class Atividade
    {
        public int Id { get; set; }               // Chave primária
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        public string Link { get; set; }

        // Coleção de PDFs (opcional, só usado para Transparencia)
        public List<ArquivoPdf> Pdfs { get; set; }

        public Atividade()
        {
            Data = DateTime.Now;
            Pdfs = new List<ArquivoPdf>();
        }
    }
}
