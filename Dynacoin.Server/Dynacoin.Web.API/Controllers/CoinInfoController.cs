using CsvHelper;
using CsvHelper.Configuration;
using Dynacoin.Domain.Model;
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
                var coinBalances = ParseCoinsRequestFile(file).Select(c => new CoinBalance
                { 
                    Symbol = c.Symbol.ToUpper(), // support case insensitive Ticker Symbol matching
                    InitialPriceUsd = c.InitialPriceUsd,
                    Amount = c.Amount
                });

                var portfolio = await coinInfoService.GetPortfolioSummaryAsync(coinBalances);

                return Ok(portfolio);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving coin data.");
                throw;
            }
        }

        private List<CoinRequestModel> ParseCoinsRequestFile(IFormFile file)
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