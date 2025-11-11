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
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeRepo _atividadeRepo;
        private readonly IWebHostEnvironment _env;

        public AtividadeController(IAtividadeRepo atividadeRepo, IWebHostEnvironment env)
        {
            _atividadeRepo = atividadeRepo;
            _env = env;
        }

        // GET /api/Atividade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atividade>>> Get()
        {
            var dados = await _atividadeRepo.TodasAtividades();
            return Ok(dados);
        }

        // GET /api/Atividade/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Atividade>> GetPorId(int id)
        {
            var atividade = await _atividadeRepo.UmaAtividade(id);
            if (atividade == null)
                return NotFound(new { erro = "Atividade não encontrada." });

            return Ok(atividade);
        }

        // POST /api/Atividade
        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50_000_000)]
        public async Task<ActionResult<Atividade>> Post([FromForm] AtividadeDTO nova)
        {
            if (nova == null)
                return BadRequest(new { erro = "Dados enviados são inválidos." });

            try
            {
                var atividade = new Atividade
                {
                    Nome = nova.Nome,
                    Descricao = nova.Descricao,
                    Data = DateTime.Now,
                    Pdfs = new List<ArquivoPdf>()
                };

                if (nova.Pdfs != null && nova.Pdfs.Count > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    foreach (var file in nova.Pdfs)
                    {
                        if (file.Length > 50_000_000)
                            return StatusCode(413, new { erro = "Arquivo excede o limite de 50 MB." });

                        var nomeLimpo = Path.GetFileNameWithoutExtension(file.FileName)
                            .Replace(" ", "_");

                        if (nomeLimpo.Length > 40)
                            nomeLimpo = nomeLimpo.Substring(0, 40);

                        var nomeFinal = $"{nomeLimpo}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(uploadDir, nomeFinal);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);

                        atividade.Pdfs.Add(new ArquivoPdf { Nome = nomeFinal });

                        if (string.IsNullOrEmpty(atividade.Link))
                            atividade.Link = $"uploads/{nomeFinal}";
                    }
                }

                var resultado = await _atividadeRepo.AdicionarAtividade(atividade);

                return CreatedAtAction(nameof(GetPorId), new { id = resultado.Id }, new
                {
                    mensagem = "Atividade criada com sucesso.",
                    resultado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao criar atividade.", detalhe = ex.Message });
            }
        }

        // DOWNLOAD /api/Atividade/download/{fileName}
        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            fileName = fileName.Replace("uploads/", "");

            var path = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", fileName);

            if (!System.IO.File.Exists(path))
                return NotFound(new { erro = "Arquivo não encontrado." });

            return PhysicalFile(path, "application/pdf", fileName);
        }

        // DELETE /api/Atividade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var atividade = await _atividadeRepo.UmaAtividade(id);

            if (atividade == null)
                return NotFound(new { erro = "Atividade não encontrada." });

            if (atividade.Pdfs != null && atividade.Pdfs.Count > 0)
            {
                foreach (var pdf in atividade.Pdfs)
                {
                    var path = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", pdf.Nome);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
            }

            await _atividadeRepo.ApagarAtividade(id);
            return Ok(new { mensagem = "Atividade excluída com sucesso." });
        }
    }
}
