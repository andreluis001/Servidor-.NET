using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;

namespace ServidorProjeto.Repositories
{
    public class OuvidoriaRepo : IOuvidoriaRepo
    {
        private readonly SistemaBD _db;

        public OuvidoriaRepo(SistemaBD db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Ouvidoria>> BuscarTodos()
        {
            return await _db.Ouvidorias.ToListAsync();
        }

        public async Task<Ouvidoria?> BuscarPorId(int id)
        {
            return await _db.Ouvidorias.FindAsync(id);
        }

        public async Task<Ouvidoria> Adicionar(Ouvidoria novaOuvidoria)
        {
            _db.Ouvidorias.Add(novaOuvidoria);
            await _db.SaveChangesAsync();
            return novaOuvidoria;
        }
    }
}
