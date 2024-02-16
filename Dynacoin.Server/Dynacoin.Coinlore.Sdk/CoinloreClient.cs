using Dynacoin.Coinlore.Sdk.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dynacoin.Coinlore.Sdk
{
    public class CoinloreClient
    {
        private const string ApiRootUrl = "https://api.coinlore.com/api";

        private readonly static JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public async Task<IEnumerable<Ticker>> GetMultipleTickerAsync(IEnumerable<int> coinIds)
        {
            return await ExecuteApiRequestAsync<IEnumerable<Ticker>>($"ticker/?id={string.Join(',', coinIds)}");
        }

        public async Task<TickersResponse> GetTickersAsync(int start = 0, int limit = 100)
        {
            return await ExecuteApiRequestAsync<TickersResponse>($"tickers/?start={start}&limit={limit}");
        }

        private async Task<T> ExecuteApiRequestAsync<T>(string url)
        {
            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{ApiRootUrl}/{url}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"CoinloreClient request failed with StatusCode: {response.StatusCode}");

                string jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(jsonResponse, DefaultJsonSerializerOptions);
            }
            catch (Exception ex)
            {
                throw new Exception("CoinloreClient request failed", ex);
            }
        }
    }
}