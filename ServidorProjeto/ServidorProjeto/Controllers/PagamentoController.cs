using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using ServidorProjeto.Models;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        [HttpPost("criar")]
        public async Task<IActionResult> CriarPagamento([FromBody] PagamentoDto dto)
        {
            if (dto == null || dto.Valor <= 0 || string.IsNullOrEmpty(dto.Email))
                return BadRequest(new { message = "Dados inválidos" });

            // Converte nascimento se necessário
            DateTime nascimento;
            if (!DateTime.TryParse(dto.Nascimento, out nascimento))
                return BadRequest(new { message = "Data de nascimento inválida" });

            var request = new PaymentCreateRequest
            {
                TransactionAmount = dto.Valor,
                Description = $"Doação de {dto.Nome}",
                PaymentMethodId = "pix",
                Payer = new PaymentPayerRequest { Email = dto.Email }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request);

            return Ok(new
            {
                payment.Id,
                payment.Status,
                payment.PointOfInteraction?.TransactionData?.QrCode,
                payment.PointOfInteraction?.TransactionData?.QrCodeBase64
            });
        }
    }
}
