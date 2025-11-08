using ServidorProjeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Repositories.Interfaces
{
    public interface IArquivoPdfRepo
    {
        Task<List<ArquivoPdf>> TodosArquivos();
        Task<ArquivoPdf> UmArquivo(int id);
        Task<ArquivoPdf> AdicionarArquivo(ArquivoPdf novoArquivo);
        Task<ArquivoPdf> AlterarArquivo(ArquivoPdf arquivoAtualizado, int id);
        Task<bool> ApagarArquivo(int id);
    }
}
