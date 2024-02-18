namespace Dynacoin.Domain.Model
{
    public class CoinSummary
    {
        public required string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal InitialPriceUsd { get; set; }

        public double ChangeUsdPercent
        {
            get
            {
                if (InitialPriceUsd == 0)
                    return 0;

                return (double)((PriceUsd - InitialPriceUsd) / InitialPriceUsd * 100);
            }
        }
    }
}