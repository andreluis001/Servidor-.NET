using System;
using System.Collections.Generic;
using ServidorProjeto;
using ServidorProjeto.Models;

namespace ServidorProjeto.Services
{
    public class TransparenciaService
    {
        private Transparencia transparencia;

        public TransparenciaService(Transparencia transparencia)
        {
            this.transparencia = transparencia;
        }

        // Retorna lista de PDFs
        public List<ArquivoPdf> ObterPdfs()
        {
            return transparencia.Pdfs;
        }

        // Ordena PDFs pelo Id usando BubbleSort
        public void BubbleSortPdfs()
        {
            var pdfs = transparencia.Pdfs;
            int n = pdfs.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (pdfs[j].Id > pdfs[j + 1].Id)
                    {
                        var temp = pdfs[j];
                        pdfs[j] = pdfs[j + 1];
                        pdfs[j + 1] = temp;
                    }
                }
            }
        }
    }
}
