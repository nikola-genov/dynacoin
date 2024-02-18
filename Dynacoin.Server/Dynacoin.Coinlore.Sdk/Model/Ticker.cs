using System.Text.Json.Serialization;

namespace Dynacoin.Coinlore.Sdk.Model
{
    public class Ticker
    {
        public int Id { get; set; }
        public required string Symbol { get; set; }
        public required string Name { get; set; }
        public required string NameId { get; set; }
        public int Rank { get; set; }

        [JsonPropertyName("price_usd")]
        public decimal PriceUsd { get; set; }

        [JsonPropertyName("price_btc")]
        public decimal PriceBtc { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public double PercentChange24h { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public double PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public double PercentChange7d { get; set; }


        [JsonPropertyName("market_cap_usd")]
        public double MarketCapUsd { get; set; }

        public double Volume24 { get; set; }
        public double Volume24a { get; set; }
        public double CSupply { get; set; }
        public double TSupply { get; set; }
    }
}
