using Dynacoin.Coinlore.Sdk;
using Dynacoin.Coinlore.Sdk.Model;
using Dynacoin.Domain.Model;
using Dynacoin.Domain.Services;

namespace Dynacoin.Services
{
    /// <summary>
    /// Implementation of the ICoinInfoService that uses Coinlore as a data provider
    /// </summary>
    public class CoinloreInfoService : ICoinInfoService
    {
        public async Task<IEnumerable<CoinInfo>> GetCoinInfosAsync(IEnumerable<string> tickerSymbols)
        {
            if (tickerSymbols == null || !tickerSymbols.Any())
                return Enumerable.Empty<CoinInfo>();

            // TODO - handle the case when a given ticker symbol is missing in the map...
            var coinloreCoinIds = tickerSymbols.Select(t => TickerMap.TickerIds[t]);

            // TODO - use DI...
            IEnumerable<Ticker> tickers = await new CoinloreClient()
                .GetMultipleTickerAsync(coinloreCoinIds)
                .ConfigureAwait(false);
            
            return tickers.Select(t => new CoinInfo
            {
                Symbol = t.Symbol,
                Price = t.PriceUsd
            })
            .ToList();
        }
    }
}
