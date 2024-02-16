using CsvHelper;
using CsvHelper.Configuration;
using Dynacoin.Domain.Services;
using Dynacoin.Web.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Dynacoin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinInfoController(ICoinInfoService coinInfoService, ILogger<CoinInfoController> logger) : ControllerBase
    {
        private const string DefaultCsvDelimiter = "|";


        [HttpPost(Name = "CoinInfo")]
        public async Task<ActionResult> GetCoinInfoAsync(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            try
            {
                var requestedCoins = ParseCoinsRequestFile(file);
                var requestedCoinTickers = requestedCoins.Select(c => c.Symbol).ToList();

                var coinInfos = await coinInfoService.GetCoinInfosAsync(requestedCoinTickers);
                
                return Ok(coinInfos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving coin data.");
                throw;
            }
        }

        private IEnumerable<CoinRequestModel> ParseCoinsRequestFile(IFormFile file)
        {
            using var streamReader = new StreamReader(file.OpenReadStream());
            
            using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                Delimiter = DefaultCsvDelimiter,
                HasHeaderRecord = false
            });
            
            return csvReader.GetRecords<CoinRequestModel>().ToList();
        }
    }
}