using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories
{
    public class TipoAtividadeRepo : ITipoAtividadeRepo
    {
        private readonly SistemaBD _dbContext;

        public TipoAtividadeRepo(SistemaBD dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TipoAtv>> BuscarTodos()
        {
            return await _dbContext.TiposAtv.ToListAsync();
        }

        public async Task<TipoAtv> BuscarPorId(int id)
        {
            return await _dbContext.TiposAtv.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TipoAtv> Adicionar(TipoAtv novoTipo)
        {
            await _dbContext.TiposAtv.AddAsync(novoTipo);
            await _dbContext.SaveChangesAsync();
            return novoTipo;
        }

        public async Task<TipoAtv> Atualizar(TipoAtv tipoAtualizado)
        {
            var existente = await _dbContext.TiposAtv.FindAsync(tipoAtualizado.Id);
            if (existente == null) throw new KeyNotFoundException("Tipo não encontrado");

            existente.Nome = tipoAtualizado.Nome;

            _dbContext.TiposAtv.Update(existente);
            await _dbContext.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> Apagar(int id)
        {
            var existente = await _dbContext.TiposAtv.FindAsync(id);
            if (existente == null) return false;

            _dbContext.TiposAtv.Remove(existente);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
