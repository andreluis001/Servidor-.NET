using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Helpers;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;

namespace ServidorProjeto.Repositories
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly SistemaBD _context;

        public UsuarioRepo(SistemaBD context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> TodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> UmUsuario(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> AdicionarUsuario(Usuario usuario)
        {
            usuario.IsAdm = usuario.Email.EndsWith("@alma.com", StringComparison.OrdinalIgnoreCase);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> AlterarUsuario(Usuario usuario, int id)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
                return null;

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioExistente.Senha = usuario.Senha;
            }

            await _context.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task<bool> ApagarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        // 
        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> ResetarSenha(string email, string novaSenha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (usuario == null)
                return false;

            usuario.Senha = CryptoHelper.Encrypt(novaSenha);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
