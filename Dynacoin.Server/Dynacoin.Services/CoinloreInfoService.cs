using Dynacoin.Coinlore.Sdk;
using Dynacoin.Coinlore.Sdk.Model;
using Dynacoin.Domain.Model;
using Dynacoin.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Dynacoin.Services
{
    /// <summary>
    /// Implementation of the ICoinInfoService that uses Coinlore as a data provider
    /// </summary>
    public class CoinloreInfoService(ICoinloreClient coinloreClient, ILogger<CoinloreInfoService> logger) : ICoinInfoService
    {
        public async Task<IEnumerable<Coin>> GetCoinsAsync(IEnumerable<string> tickerSymbols)
        {
            if (tickerSymbols == null || !tickerSymbols.Any())
                return Enumerable.Empty<Coin>();

            // TODO - handle the case when a given ticker symbol is missing in the map...
            var coinloreCoinIds = tickerSymbols.Select(t => TickerMap.TickerIds[t]);

            logger.LogInformation($"Executing request to Coinlore API with ticker IDs: {string.Join(',', coinloreCoinIds)}");

            IEnumerable<Ticker> tickers = await coinloreClient
                .GetMultipleTickerAsync(coinloreCoinIds)
                .ConfigureAwait(false);
            
            return tickers.Select(t => new Coin
            {
                Symbol = t.Symbol,
                PriceUsd = t.PriceUsd
            })
            .ToList();
        }

        public async Task<PortfolioSummary> GetPortfolioSummaryAsync(IEnumerable<CoinBalance> coinBalances)
        {
            var coinTickers = coinBalances.Select(c => c.Symbol).ToList();

            IEnumerable<Coin> coins = await GetCoinsAsync(coinTickers).ConfigureAwait(false);

            return CalculatePortfolioSummary(coins, coinBalances);
        }

        public PortfolioSummary CalculatePortfolioSummary(IEnumerable<Coin> coins, IEnumerable<CoinBalance> coinBalances)
        {
            if (coins == null || coinBalances == null)
                throw new ArgumentException($"Error calculating Portfolio Summary. Both {nameof(coins)} and {nameof(coinBalances)} must not be null.");

            if (coins.Count() != coinBalances.Count())
                throw new ArgumentException($"Error calculating Portfolio Summary. Both collections {nameof(coins)} and {nameof(coinBalances)} must be of equal length.");

            // TODO - handle the case when the same Symbol is present more than once in the list. Throw an exception or sum up the balance???

            var coinData = from coin in coins
                           join balance in coinBalances on coin.Symbol equals balance.Symbol
                           select new
                           {
                               coin.Symbol,
                               coin.PriceUsd,
                               balance.InitialPriceUsd,
                               balance.Amount
                           };

            return new PortfolioSummary
            {
                InitialValueUsd = coinData.Sum(c => c.InitialPriceUsd * c.Amount),
                TotalValueUsd = coinData.Sum(c => c.PriceUsd * c.Amount),

                Coins = coinData.Select(c => new CoinSummary 
                { 
                    Symbol = c.Symbol,
                    Amount = c.Amount,
                    PriceUsd = c.PriceUsd,
                    InitialPriceUsd = c.InitialPriceUsd,
                })
                .ToList(),
            };
        }
    }
}
