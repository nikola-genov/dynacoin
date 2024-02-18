namespace Dynacoin.Domain.Model
{
    public class Coin
    {
        public required string Symbol { get; set; }
        public decimal PriceUsd { get; set; }
    }
}
