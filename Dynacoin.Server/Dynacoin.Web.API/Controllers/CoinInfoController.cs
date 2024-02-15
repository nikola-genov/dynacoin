using Dynacoin.Domain.Model;
using Dynacoin.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dynacoin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinInfoController : ControllerBase
    {
        private readonly ICoinInfoService _coinInfoService;

        private readonly ILogger<CoinInfoController> _logger;

        public CoinInfoController(ICoinInfoService coinInfoService, ILogger<CoinInfoController> logger)
        {
            _coinInfoService = coinInfoService;
            _logger = logger;
        }

        [HttpGet(Name = "GetCoinInfo")]
        public IEnumerable<CoinInfo> Get()
        {
            return _coinInfoService.GetCoinInfos();
        }
    }
}
