﻿namespace Dynacoin.Web.API.Model
{
    public class CoinRequestModel
    {
        public decimal Amount { get; set; }
        public required string Symbol { get; set; }
        public decimal InitialPriceUsd { get; set; }
    }
}
