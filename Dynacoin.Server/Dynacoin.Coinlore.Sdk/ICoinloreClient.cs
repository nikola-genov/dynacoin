using Dynacoin.Coinlore.Sdk.Model;

namespace Dynacoin.Coinlore.Sdk
{
    public interface ICoinloreClient
    {
        Task<IEnumerable<Ticker>> GetMultipleTickerAsync(IEnumerable<int> coinIds);
        Task<TickersResponse> GetTickersAsync(int start = 0, int limit = 100);
    }
}