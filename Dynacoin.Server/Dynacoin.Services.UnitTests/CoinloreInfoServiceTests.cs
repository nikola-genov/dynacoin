using Dynacoin.Coinlore.Sdk;
using Dynacoin.Coinlore.Sdk.Model;
using Dynacoin.Domain.Model;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dynacoin.Services.UnitTests
{
    public class CoinloreInfoServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetPortfolioSummaryAsync_ReturnsCorrectSummary()
        {
            // Arrange
            var coinBalances = new List<CoinBalance>
            {
                new() { Symbol = "BTC", Amount = 1.5m, InitialPriceUsd = 24000 },
                new() { Symbol = "ETH", Amount = 3.2m, InitialPriceUsd = 800 },
            };

            var coinloreClientMock = new Mock<ICoinloreClient>();
            coinloreClientMock
                .Setup(c => c.GetMultipleTickerAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<Ticker>
                {
                    new() { Symbol = "BTC", PriceUsd = 48000, Name="btc", NameId = "btc" },
                    new() { Symbol = "ETH", PriceUsd =  1400, Name="eth", NameId = "ethc" }
                });

            var coinloreInfoService = new CoinloreInfoService(coinloreClientMock.Object, new Mock<ILogger<CoinloreInfoService>>().Object);

            // Act
            var summary = await coinloreInfoService.GetPortfolioSummaryAsync(coinBalances);

            // Assert
            Assert.That(summary.InitialValueUsd, Is.EqualTo(38560), "InitialValueUsd is incorrect");
            Assert.That(summary.TotalValueUsd, Is.EqualTo(76480), "TotalValueUsd is incorrect");
            Assert.That(summary.ChangeUsdPercent, Is.EqualTo(98.3402489626556), "ChangeUsdPercent is incorrect");
            
            Assert.That(summary.Coins, Has.Count.EqualTo(2), "Coins Count is incorrect");

            Assert.That(summary.Coins[0].InitialPriceUsd, Is.EqualTo(24000), "InitialPriceUsd is incorrect");
            Assert.That(summary.Coins[0].PriceUsd, Is.EqualTo(48000), "PriceUsd is incorrect");
            Assert.That(summary.Coins[0].ChangeUsdPercent, Is.EqualTo(100), "ChangeUsdPercent is incorrect");

            Assert.That(summary.Coins[1].InitialPriceUsd, Is.EqualTo(800), "InitialPriceUsd is incorrect");
            Assert.That(summary.Coins[1].PriceUsd, Is.EqualTo(1400), "PriceUsd is incorrect");
            Assert.That(summary.Coins[1].ChangeUsdPercent, Is.EqualTo(75), "ChangeUsdPercent is incorrect");
        }
    }
}