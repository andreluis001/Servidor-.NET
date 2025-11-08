using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.Repositories.Interfaces;
using ServidorProjeto.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;

        public LoginController(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                return BadRequest(new { erro = "E-mail e senha são obrigatórios." });

            try
            {
                var usuarios = await _usuarioRepo.TodosUsuarios();
                var usuario = usuarios.FirstOrDefault(u => u.Email == request.Email);

                if (usuario == null)
                    return Unauthorized(new { erro = "E-mail ou senha incorretos." });

                string senhaDescriptografada;

                try
                {
                    senhaDescriptografada = CryptoHelper.Decrypt(usuario.Senha);
                }
                catch
                {
                    return StatusCode(500, new { erro = "Erro ao validar senha. Contate o suporte." });
                }

                if (request.Senha != senhaDescriptografada)
                    return Unauthorized(new { erro = "E-mail ou senha incorretos." });

                return Ok(new
                {
                    mensagem = usuario.IsAdm
                        ? "Login de administrador realizado com sucesso!"
                        : "Login de usuário realizado com sucesso!",
                    usuario = new
                    {
                        usuario.Id,
                        usuario.Nome,
                        usuario.Email,
                        IsAdm = usuario.IsAdm
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = "Erro interno ao processar login.",
                    detalhe = ex.Message // pode remover se quiser não exibir detalhes
                });
            }
        }
    }
}
