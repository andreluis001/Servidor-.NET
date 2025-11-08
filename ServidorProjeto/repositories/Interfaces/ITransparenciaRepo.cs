using ServidorProjeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface ITransparenciaRepo
    {
        Task<List<Transparencia>> TodosTransparencias();
        Task<Transparencia> UmaTransparencia(int id);
        Task<Transparencia> AdicionarTransparencia(Transparencia transparencia);
        Task<Transparencia> AlterarTransparencia(Transparencia transparencia, int id);
        Task<bool> ApagarTransparencia(int id);
    }
}
