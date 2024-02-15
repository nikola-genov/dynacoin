using Microsoft.AspNetCore.Mvc;

namespace Dynacoin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinInfoController : ControllerBase
    {
        private readonly ILogger<CoinInfoController> _logger;

        public CoinInfoController(ILogger<CoinInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCoinInfo")]
        public IEnumerable<CoinInfo> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new CoinInfo
            {
                Symbol = $"BTC{index}",
                Price = 50000
            })
            .ToArray();
        }
    }
}
