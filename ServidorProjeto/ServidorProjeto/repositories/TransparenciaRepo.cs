using ServidorProjeto.Repositories.Interfaces;
using ServidorProjeto.Models;
using ServidorProjeto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories
{
    public class TransparenciaRepo : ITransparenciaRepo
    {
        private readonly SistemaBD _dbContext;

        public TransparenciaRepo(SistemaBD dbContext)
        {
            _dbContext = dbContext;
        }

        // Adiciona uma transparência
        public async Task<Transparencia> AdicionarTransparencia(Transparencia transparencia)
        {
            await _dbContext.Atividades.AddAsync(transparencia); // TPH: Transparencia é Atividade
            await _dbContext.SaveChangesAsync();
            return transparencia;
        }

        // Altera uma transparência existente
        public async Task<Transparencia> AlterarTransparencia(Transparencia transparencia, int id)
        {
            var t = await _dbContext.Atividades
                                    .OfType<Transparencia>()
                                    .Include(x => x.Pdfs)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (t == null) throw new KeyNotFoundException($"Transparência não encontrada - ID {id}");

            // Atualiza campos
            t.Titulo = transparencia.Titulo;
            t.CaminhoArquivo = transparencia.CaminhoArquivo;
            t.NomeOriginal = transparencia.NomeOriginal;
            t.DataUpload = transparencia.DataUpload;
            t.Pdfs = transparencia.Pdfs ?? new List<ArquivoPdf>();

            _dbContext.Atividades.Update(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        // Retorna todas as transparências
        public async Task<List<Transparencia>> TodosTransparencias()
        {
            return await _dbContext.Atividades
                                   .OfType<Transparencia>()
                                   .Include(x => x.Pdfs)
                                   .ToListAsync();
        }

        // Retorna uma transparência por ID
        public async Task<Transparencia> UmaTransparencia(int id)
        {
            return await _dbContext.Atividades
                                   .OfType<Transparencia>()
                                   .Include(x => x.Pdfs)
                                   .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Remove uma transparência
        public async Task<bool> ApagarTransparencia(int id)
        {
            var t = await _dbContext.Atividades
                                    .OfType<Transparencia>()
                                    .Include(x => x.Pdfs)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (t == null) throw new KeyNotFoundException($"Transparência não encontrada - ID {id}");

            _dbContext.Atividades.Remove(t);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
