using ServidorProjeto.Models;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface IOuvidoriaRepo
    {
        Task<IEnumerable<Ouvidoria>> BuscarTodos();
        Task<Ouvidoria?> BuscarPorId(int id);
        Task<Ouvidoria> Adicionar(Ouvidoria novaOuvidoria);
    }
}
