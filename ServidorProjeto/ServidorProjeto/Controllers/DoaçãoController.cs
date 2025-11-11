using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using MercadoPago.Client.Common;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Options;
using ServidorProjeto.Configurations;

namespace ServidorProjeto.Controllers
{
    public class DoaçãoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
[ApiController]
    [Route("[controller]")]
    public class DoacaoController : ControllerBase
    {
        private readonly PreferenceClient _preferenceClient;
        private readonly string _accessToken;

        // Injeção de dependência do PreferenceClient (se estiver usando .NET Core/5+)
        public DoacaoController(PreferenceClient preferenceClient, IOptions<MercadoPagoSettings> mpOptions)
        {
            _preferenceClient = preferenceClient;
            _accessToken = mpOptions.Value.AccessToken;
            // Se usar a configuração estática, inicialize aqui ou no Startup:
            // MercadoPago.SDK.Configure(_accessToken);
        }

        // Modelo simples para receber a doação
        public class DoacaoModel
        {
            public decimal Valor { get; set; }
            // Outros campos como nome do doador, email, etc.
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarDoacao([FromBody] DoacaoModel doacao)
        {
            if (doacao.Valor <= 0)
            {
                return BadRequest("O valor da doação deve ser positivo.");
            }

            // 1. Criar o item (a doação)
            var itemRequest = new PreferenceItemRequest
            {
                Title = "Doação para a ONG",
                Description = "Apoio financeiro à causa da ONG",
                Quantity = 1,
                CurrencyId = "BRL", // Código da moeda (BRL para Real Brasileiro)
                UnitPrice = doacao.Valor
            };

            // 2. Definir as URLs de retorno (Callback)
            // ESSENCIAL: O Mercado Pago redireciona o usuário para essas URLs após o pagamento.
            var backUrls = new PreferenceBackUrlsRequest
            {
                Success = "https://suaong.org/doacao/sucesso", // Para onde o usuário volta se pagar
                Pending = "https://suaong.org/doacao/pendente", // Caso o pagamento fique pendente
                Failure = "https://suaong.org/doacao/falha" // Se o pagamento for recusado
            };

            // 3. Criar o objeto principal de Preferência
            var request = new PreferenceCreateRequest
            {
                Items = new List<PreferenceItemRequest> { itemRequest },
                BackUrls = backUrls,
                AutoReturn = "approved", // Redireciona automaticamente após aprovação
                                         // ... Outras configurações, como notificações (Webhooks)
            };

            try
            {
                // Se estiver usando o cliente injetado (mais moderno)
                Preference preference = await _preferenceClient.CreateAsync(request);

                // Retorna a URL de checkout para o frontend
                return Ok(new { checkoutUrl = preference.InitPoint });
            }
            catch (Exception ex)
            {
                // Logar o erro
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao criar preferência de pagamento no Mercado Pago.");
            }
        }
    }
}
}
