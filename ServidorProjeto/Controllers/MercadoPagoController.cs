using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Options;
using ServidorProjeto.Configurations;
using ServidorProjeto.Configurations;

namespace ServidorProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MercadoPagoController : ControllerBase
    {
        private readonly MercadoPagoSettings _mpSettings;

        public MercadoPagoController(IOptions<MercadoPagoSettings> mpSettings)
        {
            _mpSettings = mpSettings.Value;
            MercadoPagoConfig.AccessToken = _mpSettings.AccessToken;
        }

        [HttpPost("criar-preferencia")]
        public async Task<IActionResult> CriarPreferencia()
        {
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Doação SYP",
                        Quantity = 1,
                        UnitPrice = 10, // valor fixo teste
                        CurrencyId = "BRL"
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://syp.com.br/sucesso",
                    Failure = "https://syp.com.br/falha",
                    Pending = "https://syp.com.br/pendente"
                },
                AutoReturn = "approved"
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            return Ok(new
            {
                init_point = preference.InitPoint,
                id = preference.Id
            });
        }
    }
}
