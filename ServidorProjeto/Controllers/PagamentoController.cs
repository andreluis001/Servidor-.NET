using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        [HttpPost("criar")]
        public async Task<IActionResult> CriarPagamento(decimal valor, string email)
        {
            var request = new PaymentCreateRequest
            {
                TransactionAmount = valor,
                Description = "Doação para Campanha",
                PaymentMethodId = "pix",
                Payer = new PaymentPayerRequest { Email = email }
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
