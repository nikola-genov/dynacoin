using Dynacoin.Domain.Model;

namespace Dynacoin.Domain.Services
{
    public interface ICoinInfoService
    {
        Task<IEnumerable<CoinInfo>> GetCoinInfosAsync(IEnumerable<string> tickerSymbols);
    }
}
