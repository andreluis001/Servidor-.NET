using ServidorProjeto;
using ServidorProjeto.Models;
using System.Collections.Generic;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface IAtividadeRepo
    {
        Task<Atividade> AdicionarAtividade(Atividade novaAtividade);
        Task<Atividade> AlterarAtividade(Atividade alteraAtividade, int id);
        Task<bool> ApagarAtividade(int id);
        Task<Atividade> UmaAtividade(int id);
        Task<List<Atividade>> TodasAtividades();
    }
}
