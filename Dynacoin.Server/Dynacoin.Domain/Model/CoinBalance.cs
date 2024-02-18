namespace Dynacoin.Domain.Model
{
    public class CoinBalance
    {
        public required string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal InitialPriceUsd { get; set; }
    }
}
