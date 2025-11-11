using ServidorProjeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface ITipoAtividadeRepo
    {
        Task<List<TipoAtv>> BuscarTodos();
        Task<TipoAtv> BuscarPorId(int id);
        Task<TipoAtv> Adicionar(TipoAtv novoTipo);
        Task<TipoAtv> Atualizar(TipoAtv tipoAtualizado);
        Task<bool> Apagar(int id);
    }
}

