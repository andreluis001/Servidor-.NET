using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.DTOs;
using ServidorProjeto.Models;
using ServidorProjeto.Repositories.Interfaces;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransparenciaController : ControllerBase
    {
        private readonly ITransparenciaRepo _repo;
        private readonly IWebHostEnvironment _env;

        public TransparenciaController(ITransparenciaRepo repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transparencia>>> GetTransparencias()
        {
            var dados = await _repo.TodosTransparencias();
            return Ok(dados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transparencia>> GetPorId(int id)
        {
            var dado = await _repo.UmaTransparencia(id);
            if (dado == null)
                return NotFound(new { mensagem = "Registro não encontrado." });

            return Ok(dado);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50_000_000)] // 50MB
        public async Task<IActionResult> PostTransparencia([FromForm] TransparenciaDTO dto)
        {
            // valida campos principais
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Titulo))
                return BadRequest(new { mensagem = "Nome e título são obrigatórios." });

            // valida tamanho total
            if (Request.ContentLength.HasValue && Request.ContentLength > 50_000_000)
                return BadRequest(new { mensagem = "O tamanho total excede 50MB." });

            var nova = new Transparencia
            {
                Nome = dto.Nome,
                Titulo = dto.Titulo,
                Descricao = dto.DescricaoTransparencia,
                DataUpload = DateTime.Now,
                Pdfs = new List<ArquivoPdf>()
            };

            if (dto.Pdfs != null && dto.Pdfs.Count > 0)
            {
                var uploadDir = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                foreach (var file in dto.Pdfs)
                {
                    if (file.Length > 50_000_000)
                        return BadRequest(new { mensagem = "Um dos arquivos excede 50MB." });

                    var nomeLimpo = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "_");
                    if (nomeLimpo.Length > 40)
                        nomeLimpo = nomeLimpo.Substring(0, 40);

                    var nomeFinal = $"{nomeLimpo}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadDir, nomeFinal);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    nova.Pdfs.Add(new ArquivoPdf { Nome = nomeFinal });

                    if (string.IsNullOrEmpty(nova.CaminhoArquivo))
                    {
                        nova.CaminhoArquivo = $"uploads/{nomeFinal}";
                        nova.Link = $"uploads/{nomeFinal}";
                    }
                }
            }

            var resultado = await _repo.AdicionarTransparencia(nova);

            return Ok(new
            {
                mensagem = "Enviado com sucesso!",
                dados = resultado
            });
        }

        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            fileName = fileName.Replace("uploads/", "");

            var path = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", fileName);
            if (!System.IO.File.Exists(path))
                return NotFound(new { mensagem = "Arquivo não encontrado." });

            return PhysicalFile(path, "application/pdf", fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dado = await _repo.UmaTransparencia(id);

            if (dado == null)
                return NotFound(new { mensagem = "Transparência não encontrada." });

            if (dado.Pdfs != null)
            {
                foreach (var pdf in dado.Pdfs)
                {
                    var caminho = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", pdf.Nome);

                    if (System.IO.File.Exists(caminho))
                        System.IO.File.Delete(caminho);
                }
            }

            await _repo.ApagarTransparencia(id);

            return Ok(new { mensagem = "Registro excluído com sucesso." });
        }
    }
}
