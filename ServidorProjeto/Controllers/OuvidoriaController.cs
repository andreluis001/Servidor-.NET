using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.Repositories.Interfaces;
using ServidorProjeto.Models;

namespace ServidorProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OuvidoriaController : ControllerBase
    {
        private readonly IOuvidoriaRepo _ouvidoriaRepo;
        private const long MaxRequestSize = 50 * 1024 * 1024; // 50MB

        public OuvidoriaController(IOuvidoriaRepo ouvidoriaRepo)
        {
            _ouvidoriaRepo = ouvidoriaRepo;
        }

        // ✅ GET: api/Ouvidoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ouvidoria>>> GetOuvidorias()
        {
            var lista = await _ouvidoriaRepo.BuscarTodos();
            return Ok(lista);
        }

        // ✅ GET: api/Ouvidoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ouvidoria>> GetOuvidoria(int id)
        {
            var ouvidoria = await _ouvidoriaRepo.BuscarPorId(id);

            if (ouvidoria == null)
                return NotFound(new { mensagem = $"Ouvidoria com ID {id} não encontrada." });

            return Ok(ouvidoria);
        }

        // ✅ POST com limite de 50MB e mensagens
        [HttpPost]
        public async Task<IActionResult> PostOuvidoria([FromBody] Ouvidoria novaOuvidoria)
        {
            // 📌 Valida tamanho da requisição
            if (Request.ContentLength.HasValue && Request.ContentLength > MaxRequestSize)
                return BadRequest(new { mensagem = "O tamanho dos dados excede o limite de 50MB." });

            if (!ModelState.IsValid)
                return BadRequest(new { mensagem = "Dados inválidos.", erros = ModelState });

            novaOuvidoria.DataEnvio = DateTime.UtcNow;

            var criada = await _ouvidoriaRepo.Adicionar(novaOuvidoria);

            return Ok(new
            {
                mensagem = "Enviado com sucesso!",
                dados = criada
            });
        }


    }
}
