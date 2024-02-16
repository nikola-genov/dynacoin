namespace Dynacoin.Coinlore.Sdk.Model
{
    public class Ticker
    {
        public int Id { get; set; }
        public required string Symbol { get; set; }
        public required string Name { get; set; }
        public required string NameId { get; set; }
        public int Rank { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal PriceBtc { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange7d { get; set; }
        public double MarketCapUsd { get; set; }
        public double Volume24 { get; set; }
        public double Volume24a { get; set; }
        //public double CSupply { get; set; }
        //public double TSupply { get; set; }
        //public double MSupply { get; set; }
    }
}
