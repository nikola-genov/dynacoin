
const PortfolioDetails = ({ portfolioSummary }) => {
    return (
        <>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <caption>Portfolio</caption>
                <thead>
                    <tr>
                        <th>Total Value (USD)</th>
                        <th>Initial Value (USD)</th>
                        <th>Change</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{portfolioSummary.totalValueUsd.toFixed(4)}</td>
                        <td>{portfolioSummary.initialValueUsd.toFixed(4)}</td>
                        <td>{portfolioSummary.changeUsdPercent.toFixed(2)}%</td>
                    </tr>
                </tbody>
            </table>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <caption>Coins</caption>
                <thead>
                    <tr>
                        <th>Symbol</th>
                        <th>Amount</th>
                        <th>Price (USD)</th>
                        <th>Initial Price (USD)</th>
                        <th>Change</th>
                    </tr>
                </thead>
                <tbody>
                    {portfolioSummary.coins.map(c => <tr key={c.symbol}>
                        <td>{c.symbol}</td>
                        <td>{c.amount.toFixed(4)}</td>
                        <td>{c.priceUsd.toFixed(4)}</td>
                        <td>{c.initialPriceUsd.toFixed(4)}</td>
                        <td>{c.changeUsdPercent.toFixed(2)}%</td>
                    </tr>
                    )}
                </tbody>
            </table>
        </>
    );
};

export default PortfolioDetails;
