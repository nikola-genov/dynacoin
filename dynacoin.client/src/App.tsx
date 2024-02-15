import { useEffect, useState } from 'react';
import './App.css';

interface CoinInfo {
    symbol: string;
    price: number;
}

function App() {
    const [coinInfos, setCoinInfos] = useState<CoinInfo[]>();

    useEffect(() => {
        populateCoinData();
    }, []);

    const contents = coinInfos === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Symbol</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                {coinInfos.map(coinInfo =>
                    <tr key={coinInfo.symbol}>
                        <td>{coinInfo.symbol}</td>
                        <td>{coinInfo.price}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Coin Info</h1>
            {contents}
        </div>
    );

    async function populateCoinData() {
        const response = await fetch('coininfo');
        const data = await response.json();
        setCoinInfos(data);
    }
}

export default App;