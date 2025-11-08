using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories
{
    public class ArquivoPdfRepo : IArquivoPdfRepo
    {
        private readonly SistemaBD _dbContext;

        public ArquivoPdfRepo(SistemaBD dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ArquivoPdf>> TodosArquivos()
        {
            return await _dbContext.ArquivosPdf.ToListAsync();
        }

        public async Task<ArquivoPdf> UmArquivo(int id)
        {
            return await _dbContext.ArquivosPdf.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ArquivoPdf> AdicionarArquivo(ArquivoPdf novoArquivo)
        {
            await _dbContext.ArquivosPdf.AddAsync(novoArquivo);
            await _dbContext.SaveChangesAsync();
            return novoArquivo;
        }

        public async Task<ArquivoPdf> AlterarArquivo(ArquivoPdf arquivoAtualizado, int id)
        {
            var arquivoExistente = await _dbContext.ArquivosPdf.FirstOrDefaultAsync(a => a.Id == id);
            if (arquivoExistente == null)
                throw new Exception($"Arquivo não encontrado - ID: {id}");

            arquivoExistente.Nome = arquivoAtualizado.Nome;
            // atualizar outras propriedades se houver
            _dbContext.ArquivosPdf.Update(arquivoExistente);
            await _dbContext.SaveChangesAsync();
            return arquivoExistente;
        }

        public async Task<bool> ApagarArquivo(int id)
        {
            var arquivo = await _dbContext.ArquivosPdf.FirstOrDefaultAsync(x => x.Id == id);
            if (arquivo == null)
                throw new Exception($"Arquivo não encontrado - ID: {id}");

            _dbContext.ArquivosPdf.Remove(arquivo);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
