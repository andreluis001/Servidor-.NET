using Microsoft.AspNetCore.Mvc;
using ServidorProjeto.Repositories.Interfaces;
using ServidorProjeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServidorProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepo _contatoRepo;

        public ContatoController(IContatoRepo contatoRepo)
        {
            _contatoRepo = contatoRepo;
        }

        // ✅ GET: api/Contato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            try
            {
                var contatos = await _contatoRepo.BuscarTodos();
                return Ok(contatos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao buscar contatos.", detalhe = ex.Message });
            }
        }

        // ✅ GET: api/Contato/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            try
            {
                var contato = await _contatoRepo.BuscarPorId(id);

                if (contato == null)
                    return NotFound(new { erro = $"Contato com ID {id} não encontrado." });

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao buscar contato.", detalhe = ex.Message });
            }
        }

        // ✅ POST: api/Contato
        [HttpPost]
        public async Task<ActionResult> PostContato([FromBody] Contato novoContato)
        {
            if (novoContato == null)
                return BadRequest(new { erro = "Dados inválidos enviados." });

            try
            {
                var contatoCriado = await _contatoRepo.Adicionar(novoContato);

                return CreatedAtAction(nameof(GetContato), new
                {
                    id = contatoCriado.Id
                },
                new
                {
                    mensagem = "Contato enviado com sucesso.",
                    contato = contatoCriado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao criar contato.", detalhe = ex.Message });
            }
        }
    }
}
