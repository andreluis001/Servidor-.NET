using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ServidorProjeto.DTOs
{
    public class AtividadeDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }              // corresponde ao Nome da atividade
        public string Descricao { get; set; }
        public string Status { get; set; }            // obrigatório
        public DateTime Data { get; set; }
        public string Link { get; set; }              // obrigatório

        // Para upload de arquivos PDF
        public List<IFormFile> Pdfs { get; set; } = new List<IFormFile>();
    }
}
