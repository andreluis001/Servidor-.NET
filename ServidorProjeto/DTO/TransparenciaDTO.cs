using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServidorProjeto.DTOs
{
    public class TransparenciaDTO
    {
        // Campos enviados pelo cliente
        public string Nome { get; set; }                     // obrigatório
        public string Titulo { get; set; }                   // obrigatório
        public string DescricaoTransparencia { get; set; }   // obrigatório

        public string Status { get; set; }                  // opcional, default "Ativo"

        // Upload de múltiplos PDFs
        public List<IFormFile> Pdfs { get; set; } = new List<IFormFile>();

        // Campos preenchidos pelo backend (não aparecem no Swagger/JSON)
        [JsonIgnore] public string CaminhoArquivo { get; set; }
        [JsonIgnore] public string Link { get; set; }
        [JsonIgnore] public string NomeOriginal { get; set; }
        [JsonIgnore] public string TipoMime { get; set; }
        [JsonIgnore] public long TamanhoBytes { get; set; }
        [JsonIgnore] public DateTime DataUpload { get; set; }
    }
}
