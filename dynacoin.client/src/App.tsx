import { useEffect, useState } from 'react';
import './App.css';
import config from './config';
import CoinsFileUpload from './components/CoinsFileUpload';

// TODO - extract interfaces to external files
interface CoinBalance {
    symbol: string;
    amount: number;
    initialPriceUsd: number;
}

interface CoinSummary {
    symbol: string;
    amount: number;
    priceUsd: number;
    initialPriceUsd: number;
    changeUsdPercent: number;
}

interface PortfolioSummary {
    initialValueUsd: number;
    totalValueUsd: number;
    changeUsdPercent: number;
    coins: CoinSummary[];
}

function App() {
    const [portfolioSummary, setPortfolioSummary] = useState<PortfolioSummary>();
    
    useEffect(() => {
        const intervalId = setInterval(() => {
            console.log("refreshing portfolio");
            refreshPortfolio();
        }, config.refreshIntervalSeconds * 1000);

        // clean up the effect
        return () => {
            clearInterval(intervalId);
        };
    }, [portfolioSummary]);
    
    const handleUploadComplete = (data: PortfolioSummary) => {
        setPortfolioSummary(data);
    };

    const refreshPortfolio = () => {
        if (portfolioSummary?.coins == null || portfolioSummary.coins.length == 0)
            return;

        const coinBalances: CoinBalance[] = portfolioSummary.coins.map(c => ({
            symbol: c.symbol,
            amount: c.amount,
            initialPriceUsd: c.initialPriceUsd
        }));

        // TODO - extract endpoint to external config
        fetch('coininfo/portfolio-summary', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(coinBalances)
        })
        .then(response => response.json())
        .then(data => {
            setPortfolioSummary(data);
        })
        .catch(error => {
            console.error('Error refreshing portfolio:', error);
        });
    };

    const contents = portfolioSummary?.coins === undefined
        ? <p><em>No coins file uploaded</em></p>
        : <>
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
                        <td>{portfolioSummary.totalValueUsd}</td>
                        <td>{portfolioSummary.initialValueUsd}</td>
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
                        <td>{c.amount}</td>
                        <td>{c.priceUsd}</td>
                        <td>{c.initialPriceUsd}</td>
                        <td>{c.changeUsdPercent.toFixed(2)}%</td>
                    </tr>
                    )}
                </tbody>
            </table>
        </>;

    return (
        <div>
            <CoinsFileUpload onUploadComplete={handleUploadComplete} />
            {contents}
        </div>
    );
}

export default App;