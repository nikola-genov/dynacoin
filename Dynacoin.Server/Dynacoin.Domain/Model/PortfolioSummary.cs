namespace Dynacoin.Domain.Model
{
    public class PortfolioSummary
    {
        public decimal InitialValueUsd {  get; set; } 
        public decimal TotalValueUsd {  get; set; }

        public double ChangeUsdPercent
        {
            get
            {
                if (InitialValueUsd == 0)
                    return 0;

                return (double)((TotalValueUsd - InitialValueUsd) / InitialValueUsd * 100);
            }
        }

        public required IList<CoinSummary> Coins { get; set; }
    }
}
