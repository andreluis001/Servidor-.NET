using ServidorProjeto.Models;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface IContatoRepo
    {
        Task<IEnumerable<Contato>> BuscarTodos();
        Task<Contato?> BuscarPorId(int id);
        Task<Contato> Adicionar(Contato contato);
    }
}
