using System.Text;
using System.Text.Json;
using Tgg.Helpers;
using Tgg.Models;
using Tgg.Services.Interfaces;

namespace Tgg.Services
{
    public class ProdutoService : IProdutos
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string endpoint = "api/produtos";
        private readonly JsonSerializerOptions _options;

        private ProdutoViewModel produtoVM;
        private IEnumerable<ProdutoViewModel> prodVM;
        public ProdutoService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProdutoViewModel>> GetProdutosAsync(string token)
        {
            var client = _clientFactory.CreateClient("dynamic");

            ServicesHelpers.PutTokenInHeaderAuthorizationHelpers(token, client);

            using (var response = await client.GetAsync(endpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    prodVM = await JsonSerializer
                                    .DeserializeAsync<IEnumerable<ProdutoViewModel>>
                                    (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return prodVM;

        }

        public async Task<ProdutoViewModel> GetProdutoPorId(int id, string token)
        {
            var client = _clientFactory.CreateClient("dynamic");

            ServicesHelpers.PutTokenInHeaderAuthorizationHelpers(token, client);

            using (var response = await client.GetAsync(endpoint + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    produtoVM = await JsonSerializer
                                    .DeserializeAsync<ProdutoViewModel>
                                    (apiResponse, _options);
                }
                else
                {
                    return null;
                }


                return produtoVM;
            }
        }


        public async Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produtoVm, string token)
        {
            var client = _clientFactory.CreateClient("dynamic");

            ServicesHelpers.PutTokenInHeaderAuthorizationHelpers(token, client);

            var produto = JsonSerializer.Serialize(produtoVm);
            StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(endpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return produtoVM;
        }
        public async Task<bool> AutalizaProduto(int id, ProdutoViewModel produtoVm, string token)
        {
            var client = _clientFactory.CreateClient("dynamic");

            ServicesHelpers.PutTokenInHeaderAuthorizationHelpers(token, client);

            var produto = JsonSerializer.Serialize(produtoVm);
            StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

            using (var response = await client.PatchAsync(endpoint + "/" + id, content))
            {
                if (response.IsSuccessStatusCode) return true;

                else return false;

            }
        }


        public async Task<bool> DeletarProduto(int id, string token)
        {
            var client = _clientFactory.CreateClient("dynamic");

            ServicesHelpers.PutTokenInHeaderAuthorizationHelpers(token, client);

            using (var response = await client.DeleteAsync(endpoint + "/" + id))
            {
                if (response.IsSuccessStatusCode) return true;

                else return false;

            }
        }


    }
}
