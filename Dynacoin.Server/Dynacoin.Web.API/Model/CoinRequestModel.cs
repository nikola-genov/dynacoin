namespace Dynacoin.Web.API.Model
{
    public class CoinRequestModel
    {
        public double Amount { get; set; }
        public required string Symbol { get; set; }
        public decimal InitialPrice { get; set; }
    }
}
