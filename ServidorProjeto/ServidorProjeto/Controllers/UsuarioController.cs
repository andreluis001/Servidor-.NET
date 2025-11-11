using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;
using ServidorProjeto.Helpers;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;

        public UsuarioController(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        // ✅ GET TODOS
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var usuarios = await _usuarioRepo.TodosUsuarios();

                foreach (var u in usuarios)
                    u.Senha = CryptoHelper.Decrypt(u.Senha); // descriptografar antes de retornar

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar usuários: {ex.Message}");
            }
        }

        // ✅ GET POR ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            try
            {
                var usuario = await _usuarioRepo.UmUsuario(id);

                if (usuario == null)
                    return NotFound("Usuário não encontrado.");

                usuario.Senha = CryptoHelper.Decrypt(usuario.Senha);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar usuário: {ex.Message}");
            }
        }

        // ✅ GET POR EMAIL
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetPorEmail(string email)
        {
            try
            {
                var usuario = await _usuarioRepo.BuscarPorEmail(email);

                if (usuario == null)
                    return NotFound("Nenhum usuário encontrado com esse e-mail.");

                usuario.Senha = CryptoHelper.Decrypt(usuario.Senha);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar usuário pelo e-mail: {ex.Message}");
            }
        }

        // ✅ POST (Criar usuário)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario novoUsuario)
        {
            try
            {
                if (novoUsuario == null)
                    return BadRequest("Dados inválidos.");

                if (string.IsNullOrWhiteSpace(novoUsuario.Senha))
                    return BadRequest("Senha é obrigatória.");

                novoUsuario.Senha = CryptoHelper.Encrypt(novoUsuario.Senha);

                var usuarioCriado = await _usuarioRepo.AdicionarUsuario(novoUsuario);
                usuarioCriado.Senha = CryptoHelper.Decrypt(usuarioCriado.Senha);

                return Created("", new
                {
                    mensagem = "Usuário criado com sucesso!",
                    usuario = usuarioCriado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        // ✅ PUT (Alterar usuário)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuarioAtualizado)
        {
            try
            {
                if (usuarioAtualizado == null)
                    return BadRequest("Dados inválidos.");

                if (!string.IsNullOrWhiteSpace(usuarioAtualizado.Senha))
                    usuarioAtualizado.Senha = CryptoHelper.Encrypt(usuarioAtualizado.Senha);

                var usuarioAlterado = await _usuarioRepo.AlterarUsuario(usuarioAtualizado, id);

                if (usuarioAlterado == null)
                    return NotFound("Usuário não encontrado para atualização.");

                usuarioAlterado.Senha = CryptoHelper.Decrypt(usuarioAlterado.Senha);

                return Ok(new
                {
                    mensagem = "Usuário atualizado com sucesso!",
                    usuario = usuarioAlterado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var apagado = await _usuarioRepo.ApagarUsuario(id);

                if (!apagado)
                    return NotFound("Usuário não encontrado para exclusão.");

                return Ok(new { mensagem = "Usuário removido com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao apagar usuário: {ex.Message}");
            }
        }
        [HttpPost("reset-senha/{email}")]
        public async Task<IActionResult> ResetSenha(string email, [FromBody] ResetSenhaDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.NovaSenha))
                return BadRequest("A nova senha é obrigatória.");

            var result = await _usuarioRepo.ResetarSenha(email, model.NovaSenha);

            if (!result)
                return NotFound("Usuário não encontrado.");

            return Ok("Senha redefinida com sucesso.");
        }   
    }
}