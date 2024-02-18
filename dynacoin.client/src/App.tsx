import { useState } from 'react';
import './App.css';
import CoinsFileUpload from './components/CoinsFileUpload';

// TODO - extract interfaces to external files
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

    const handleUploadComplete = (data: PortfolioSummary) => {
        setPortfolioSummary(data);
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
                        <th>Change (%)</th>
                    </tr>
                </thead>
                <tr>
                    <td>{portfolioSummary.totalValueUsd}</td>
                    <td>{portfolioSummary.initialValueUsd}</td>
                    <td>{portfolioSummary.changeUsdPercent}</td>
                </tr>
            </table>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <caption>Coins</caption>
                <thead>
                    <tr>
                        <th>Symbol</th>
                        <th>Amount</th>
                        <th>Price (USD)</th>
                        <th>Initial Price (USD)</th>
                        <th>Change (%)</th>
                    </tr>
                </thead>
                <tbody>
                    {portfolioSummary.coins.map(c => <tr key={c.symbol}>
                        <td>{c.symbol}</td>
                        <td>{c.amount}</td>
                        <td>{c.priceUsd}</td>
                        <td>{c.initialPriceUsd}</td>
                        <td>{c.changeUsdPercent}</td>
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