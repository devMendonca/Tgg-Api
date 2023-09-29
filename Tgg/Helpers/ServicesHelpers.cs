namespace Tgg.Helpers
{
    public static class ServicesHelpers
    {
        public static void PutTokenInHeaderAuthorizationHelpers(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        
    }
}
