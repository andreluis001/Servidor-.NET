using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories
{
    public class AtividadeRepo : IAtividadeRepo
    {
        private readonly SistemaBD _dbContext;

        public AtividadeRepo(SistemaBD dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Atividade> AdicionarAtividade(Atividade novaAtividade)
        {
            await _dbContext.Atividades.AddAsync(novaAtividade);
            await _dbContext.SaveChangesAsync();
            return novaAtividade;
        }

        public async Task<Atividade> AlterarAtividade(Atividade atividade, int id)
        {
            var temp = await UmaAtividade(id);
            if (temp == null)
                throw new Exception($"Atividade não encontrada - ID:{id}");

            temp.Nome = atividade.Nome;
            temp.Descricao = atividade.Descricao;
            temp.Data = atividade.Data;
            temp.Link = atividade.Link;

            _dbContext.Atividades.Update(temp);
            await _dbContext.SaveChangesAsync();
            return temp;
        }

        public async Task<bool> ApagarAtividade(int id)
        {
            var temp = await UmaAtividade(id);
            if (temp == null)
                throw new Exception($"Atividade não encontrada - ID:{id}");

            _dbContext.Atividades.Remove(temp);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Atividade>> TodasAtividades()
        {
            return await _dbContext.Atividades
                .Include(a => a.Pdfs)
                .ToListAsync();
        }

        public async Task<Atividade> UmaAtividade(int id)
        {
            return await _dbContext.Atividades
                .Include(a => a.Pdfs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
