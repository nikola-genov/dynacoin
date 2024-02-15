using Dynacoin.Domain.Model;
using Dynacoin.Domain.Services;

namespace Dynacoin.Services
{
    /// <summary>
    /// Implementation of the ICoinInfoService that uses Coinlore as a data provider
    /// </summary>
    public class CoinloreInfoService : ICoinInfoService
    {
        public IEnumerable<CoinInfo> GetCoinInfos()
        {
            return Enumerable.Range(1, 5).Select(index => new CoinInfo
            {
                Symbol = $"BTC{index}",
                Price = 50000
            })
            .ToArray();
        }
    }
}
