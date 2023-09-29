using System.Text;
using System.Text.Json;
using Tgg.Models.Auth;
using Tgg.Services.Interfaces;

namespace Tgg.Services
{
    public class Auth : IAuth
    {
        private readonly IHttpClientFactory _clientFactory;
        const string endpoint = "/api/Autoriza/Login";

        private readonly JsonSerializerOptions _options;
        private TokenAuth UserToken;

        public Auth(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TokenAuth> AutenticaUsuario(User user)
        {
            var client = _clientFactory.CreateClient("Autentica");

            var usuario = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

            using(var response = await client.PostAsync(endpoint, content))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    UserToken = await JsonSerializer.DeserializeAsync<TokenAuth>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return UserToken;
        }
    }
}
