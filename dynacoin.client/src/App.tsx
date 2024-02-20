import { useEffect, useState } from 'react';
import './App.css';
import config from './config';
import CoinsFileUpload from './components/CoinsFileUpload';
import PortfolioDetails from './components/PortfolioDetails';
import { CoinBalance } from './models/CoinBalance';
import { PortfolioSummary } from './models/PortfolioSummary';

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
        : <PortfolioDetails portfolioSummary={portfolioSummary} />;

    return (
        <div>
            <CoinsFileUpload onUploadComplete={handleUploadComplete} />
            {contents}
        </div>
    );
}

export default App;