import { useState } from 'react';
import './App.css';
import CoinsFileUpload from './components/CoinsFileUpload';

interface CoinInfo {
    symbol: string;
    price: number;
}

function App() {
    const [coinInfos, setCoinInfos] = useState<CoinInfo[]>();

    const handleUploadComplete = (data: CoinInfo[]) => {
        setCoinInfos(data);
    };

    const contents = coinInfos === undefined
        ? <p><em>No coins file uploaded</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <caption>Coin Info</caption>
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
            <CoinsFileUpload onUploadComplete={handleUploadComplete} />
            {contents}
        </div>
    );
}

export default App;