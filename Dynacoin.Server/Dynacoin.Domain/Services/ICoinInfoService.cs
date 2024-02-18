using Dynacoin.Domain.Model;

namespace Dynacoin.Domain.Services
{
    public interface ICoinInfoService
    {
        Task<IEnumerable<Coin>> GetCoinsAsync(IEnumerable<string> tickerSymbols);
        Task<PortfolioSummary> GetPortfolioSummaryAsync(IEnumerable<CoinBalance> coinBalances);
        PortfolioSummary CalculatePortfolioSummary(IEnumerable<Coin> coins, IEnumerable<CoinBalance> coinBalances);
    }
}