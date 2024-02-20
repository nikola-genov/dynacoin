import { CoinSummary } from './CoinSummary';

export interface PortfolioSummary {
    initialValueUsd: number;
    totalValueUsd: number;
    changeUsdPercent: number;
    coins: CoinSummary[];
}
