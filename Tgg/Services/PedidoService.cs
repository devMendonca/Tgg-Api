using System.Text;
using System.Text.Json;
using Tgg.Models;
using Tgg.Services.Interfaces;

namespace Tgg.Services
{
    public class PedidoService : IPedidos
    {
        private const string endpoint = "api/pedidos";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private PedidosViewModel pedidoVM;
        private IEnumerable<PedidosViewModel> pedidosVM;

        public PedidoService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<PedidosViewModel>> GetPedidosAsync()
        {
            var client = _clientFactory.CreateClient("dynamic");

            using (var response = await client.GetAsync(endpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    pedidosVM = await JsonSerializer
                                    .DeserializeAsync<IEnumerable<PedidosViewModel>>
                                    (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return pedidosVM;
        }

        public async Task<PedidosViewModel> GetPedidosByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("dynamic");

            using (var response = await client.GetAsync(endpoint + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    pedidoVM = await JsonSerializer
                                    .DeserializeAsync<PedidosViewModel>
                                    (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return pedidoVM;
        }

        public async Task<PedidosViewModel> CriarPedidoAsyn(PedidosViewModel pedidoVM)
        {
            var client = _clientFactory.CreateClient("dynamic");

            var categoria = JsonSerializer.Serialize(pedidoVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(endpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    pedidoVM = await JsonSerializer.DeserializeAsync<PedidosViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return pedidoVM;

        }
        public async Task<bool> AtualizarPedido(int id, PedidosViewModel pedido)
        {
            var client = _clientFactory.CreateClient("dynamic");

            var categoria = JsonSerializer.Serialize(pedido);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");

            using (var response = await client.PatchAsync(endpoint + "/" + id, content))
            {
                if (response.IsSuccessStatusCode) return true;

                else return false;
               
            }
        }


        public async Task<bool> DeletarPedido(int id)
        {
            var client = _clientFactory.CreateClient("dynamic");

            using (var response = await client.DeleteAsync(endpoint + "/" + id))
            {
                if (response.IsSuccessStatusCode) return true;

                else return false;

            }
        }


     
    }
}
