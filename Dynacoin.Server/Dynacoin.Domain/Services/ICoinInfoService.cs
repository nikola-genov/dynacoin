using Dynacoin.Domain.Model;

namespace Dynacoin.Domain.Services
{
    public interface ICoinInfoService
    {
        IEnumerable<CoinInfo> GetCoinInfos();
    }
}
