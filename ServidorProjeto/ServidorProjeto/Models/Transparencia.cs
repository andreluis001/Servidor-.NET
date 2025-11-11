using ServidorProjeto.Models;
using System;
using System.Collections.Generic;

public class Transparencia : Atividade
{
    public string Titulo { get; set; }
    public string CaminhoArquivo { get; set; }
    public string NomeOriginal { get; set; }

    public DateTime DataUpload { get; set; }

    // Relacionamento com PDFs
    public List<ArquivoPdf> Pdfs { get; set; } = new List<ArquivoPdf>();
}
