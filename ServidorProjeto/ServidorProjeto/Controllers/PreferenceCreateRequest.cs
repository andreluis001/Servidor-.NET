using MercadoPago.Client.Preference;

namespace ServidorProjeto.Controllers
{
    internal class PreferenceCreateRequest : PreferenceRequest
    {
        public List<PreferenceItemRequest> Items { get; set; }
        public PreferenceBackUrlsRequest BackUrls { get; set; }
        public string AutoReturn { get; set; }
    }
}