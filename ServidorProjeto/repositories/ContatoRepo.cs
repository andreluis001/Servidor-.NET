using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;

namespace ServidorProjeto.Repositories
{
    public class ContatoRepo : IContatoRepo
    {
        private readonly SistemaBD _db;

        public ContatoRepo(SistemaBD db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Contato>> BuscarTodos()
        {
            return await _db.Contatos.ToListAsync();
        }

        public async Task<Contato?> BuscarPorId(int id)
        {
            return await _db.Contatos.FindAsync(id);
        }

        public async Task<Contato> Adicionar(Contato contato)
        {
            _db.Contatos.Add(contato);
            await _db.SaveChangesAsync();
            return contato;
        }
    }
}
