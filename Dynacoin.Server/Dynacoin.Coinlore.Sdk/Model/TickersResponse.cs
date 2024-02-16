namespace Dynacoin.Coinlore.Sdk.Model
{
    public class TickersResponse
    {
        public IEnumerable<Ticker> Data { get; set; }
        public TickersResponseInfo Info { get; set; }
    }
}