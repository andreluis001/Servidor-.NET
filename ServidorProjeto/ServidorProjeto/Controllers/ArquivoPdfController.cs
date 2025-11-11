using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArquivoPdfController : ControllerBase
    {
        private readonly IArquivoPdfRepo _arquivoPdfRepo;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        private const long MaxFileSize = 50 * 1024 * 1024; // 50MB

        public ArquivoPdfController(IArquivoPdfRepo arquivoPdfRepo)
        {
            _arquivoPdfRepo = arquivoPdfRepo;

            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest(new { mensagem = "Arquivo inválido." });

            if (arquivo.Length > MaxFileSize)
                return BadRequest(new { mensagem = "Arquivo excede o limite de 50MB." });

            if (arquivo.ContentType != "application/pdf")
                return BadRequest(new { mensagem = "Apenas arquivos PDF são permitidos." });

            var caminhoArquivo = Path.Combine(_uploadFolder, arquivo.FileName);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var novoPdf = new ArquivoPdf
            {
                Nome = arquivo.FileName
            };

            var resultado = await _arquivoPdfRepo.AdicionarArquivo(novoPdf);

            return Created("/api/ArquivoPdf/" + resultado.Id, new
            {
                mensagem = "Upload concluído com sucesso!",
                resultado.Id,
                resultado.Nome,
                CaminhoFisico = caminhoArquivo
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var arquivos = await _arquivoPdfRepo.TodosArquivos();
            return Ok(arquivos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var arquivo = await _arquivoPdfRepo.UmArquivo(id);
            if (arquivo == null)
                return NotFound(new { mensagem = "Arquivo não encontrado." });

            return Ok(arquivo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var arquivo = await _arquivoPdfRepo.UmArquivo(id);
            if (arquivo == null)
                return NotFound(new { mensagem = "Arquivo não encontrado." });

            var caminhoArquivo = Path.Combine(_uploadFolder, arquivo.Nome);

            if (System.IO.File.Exists(caminhoArquivo))
                System.IO.File.Delete(caminhoArquivo);

            await _arquivoPdfRepo.ApagarArquivo(id);

            return Ok(new { mensagem = "Arquivo removido com sucesso." });
        }
    }
}