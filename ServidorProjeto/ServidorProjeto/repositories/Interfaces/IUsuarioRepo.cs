using ServidorProjeto;
using ServidorProjeto.Models;
using ServidorProjeto.repositories;
using ServidorProjeto.Repositories.Interfaces;
namespace ServidorProjeto.Repositories.Interfaces
{
    // Interface corrigida para evitar CS0501 e padronizar nomes

    public interface IUsuarioRepo
    {
        Task<IEnumerable<Usuario>> TodosUsuarios();
        Task<Usuario> UmUsuario(int id);
        Task<Usuario> AdicionarUsuario(Usuario usuario);
        Task<Usuario> AlterarUsuario(Usuario usuario, int id); // ✅ agora usa int
        Task<bool> ApagarUsuario(int id);
        Task<Usuario> BuscarPorEmail(string email);
        Task<bool> ResetarSenha(string email, string novaSenha);


    }
}


